using Acquisition.Api.Client;

namespace LoanApplicationJourney.Bff.Infrastructure.Acquisition;

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