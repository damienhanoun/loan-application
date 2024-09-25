using Acquisition.Api.Client;

namespace Acquisition.WebApplication.Server.Infrastructure;

public static class ServicesCollectionExtension
{
    public static void AddClients(this IServiceCollection services)
    {
        services.AddHttpClient<IAcquisitionApiClient, AcquisitionApiClient>(client => new AcquisitionApiClient("https://localhost:7197", client));
    }
}