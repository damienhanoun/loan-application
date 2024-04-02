using System.Data.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Respawn;
using Testcontainers.PostgreSql;
using Xunit;

namespace Acquisition.Api.Tests.Acceptance.Drivers;

public class AcquisitionApiFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer =
        new PostgreSqlBuilder()
            .WithPortBinding(5432)
            .WithDatabase("acquisition")
            .WithUsername("postgres")
            .WithPassword("password")
            .Build();

    private DbConnection _dbConnection = null!;

    private Respawner _respawner = null!;

    public HttpClient Client { get; private set; } = null!;

    public async Task InitializeAsync()
    {
        // await _dbContainer.StartAsync();
        Client = CreateClient();
        // _dbConnection = new NpgsqlConnection(_dbContainer.GetConnectionString());
        // await _dbConnection.OpenAsync();
        // _respawner = await Respawner.CreateAsync(_dbConnection, new RespawnerOptions
        // {
        //     DbAdapter = DbAdapter.Postgres,
        //     SchemasToInclude = new[] { "public" }
        // });
    }

    public new async Task DisposeAsync()
    {
        // await _dbContainer.StopAsync();
    }

    public async Task ResetDatabaseAsync()
    {
        // await _respawner.ResetAsync(_dbConnection);
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            // services.RemoveAll<DbContextOptions<AcquisitionContext>>();
            // services.RemoveAll<AcquisitionContext>();

            // services.AddDbContext<AcquisitionContext>(x => x.UseNpgsql(_dbContainer.GetConnectionString()));
        });
    }
}