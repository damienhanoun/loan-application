using Acquisition.Api.Persistence.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;
using Respawn;
using Testcontainers.PostgreSql;

namespace Acquisition.Api.Tests.Helpers;

public class AcquisitionApiFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer =
        new PostgreSqlBuilder()
            .WithPortBinding(5432)
            .WithDatabase("acquisition")
            .WithUsername("postgres")
            .WithPassword("password")
            .Build();

    private Respawner _respawner = null!;

    public NpgsqlConnection DbConnection = null!;

    public HttpClient Client { get; private set; } = null!;

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        Client = CreateClient();
        DbConnection = new NpgsqlConnection(_dbContainer.GetConnectionString());
        await DbConnection.OpenAsync();
        _respawner = await Respawner.CreateAsync(DbConnection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.Postgres,
            SchemasToInclude = new[] { "public" }
        });
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
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