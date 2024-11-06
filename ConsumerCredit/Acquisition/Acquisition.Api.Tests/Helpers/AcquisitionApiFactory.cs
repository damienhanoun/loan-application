using Acquisition.Api.Infrastructure.Persistence.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;
using Respawn;
using Respawn.Graph;
using Testcontainers.PostgreSql;

namespace Acquisition.Api.Tests.Helpers;

public class AcquisitionApiFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer =
        new PostgreSqlBuilder()
            .WithPortBinding(5433, true) // randomness of port allow to run integration and acceptance tests at the same time
            .Build();

    private Respawner _respawner = null!;
    private NpgsqlConnection DbConnection = null!;

    public HttpClient Client { get; private set; } = null!;

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        Client = CreateClient();
        DbConnection = new NpgsqlConnection(_dbContainer.GetConnectionString());
        await DbConnection.OpenAsync();
        using (var scope = Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AcquisitionContext>();
            await dbContext.Database.MigrateAsync();
        }

        _respawner = await Respawner.CreateAsync(DbConnection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.Postgres,
            SchemasToInclude = new[] { "public" },
            TablesToInclude = new[]
            {
                // Do not reference static resources that are seeded at start
                new Table("LoanApplications"),
                new Table("LoanOffers"),
                new Table("LoanContracts")
            }
        });
    }

    public new Task DisposeAsync()
    {
        return _dbContainer.StopAsync();
    }

    public async Task ResetDatabaseAsync()
    {
        await _respawner.ResetAsync(DbConnection);
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll<DbContextOptions<AcquisitionContext>>();
            services.RemoveAll<AcquisitionContext>();

            services.AddDbContext<AcquisitionContext>(x => x.UseNpgsql(_dbContainer.GetConnectionString()));
        });
    }
}