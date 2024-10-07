using Microsoft.EntityFrameworkCore;

namespace Acquisition.Api.Infrastructure.Persistence.Database;

public static class ServiceCollectionExtensions
{
    public static void AddAcquisitionDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("AcquisitionDatabase");
        services.AddDbContext<AcquisitionContext>(
            options => options.UseNpgsql(connectionString),
            ServiceLifetime.Transient);
    }
}