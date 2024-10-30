using Acquisition.Api.Client;

namespace Acquisition.WebApplication.Server.Infrastructure.Acquisition;

public static class ServicesCollectionExtension
{
    public static void AddClients(this IServiceCollection services, ConfigurationManager builderConfiguration)
    {
        services.AddHttpClient<IAcquisitionApiClient, AcquisitionApiClient>(client =>
        {
            var url = builderConfiguration["Acquisition:Api:Public:Url"];
            return new AcquisitionApiClient(url, client);
        });
    }
}