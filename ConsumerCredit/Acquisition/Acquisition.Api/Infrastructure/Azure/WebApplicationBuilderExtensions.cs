namespace Acquisition.Api.Infrastructure.Azure;

public enum ConfigurationType
{
    AzureAppConfiguration,
    DevelopmentConfiguration
}

public static class WebApplicationBuilderExtensions
{
    private static ConfigurationType _configurationType;

    public static void AddConfiguration(this WebApplicationBuilder webApplicationBuilder, ConfigurationType configurationType)
    {
        _configurationType = configurationType;

        if (_configurationType == ConfigurationType.AzureAppConfiguration)
        {
            // Configuration.AddUserSecrets get secrets stored on the local machine that have been added way :
            // dotnet user-secrets set "AppConfig:ConnectionString" "Endpoint=https://<app configuration path>.azconfig.io;Id=<your id>;Secret=<your secret>"
            // It add a <UserSecretsId>...</UserSecretsId> in csproj
            // It is called when Development environment by default
            // but NSwag doesn't use launchSettings.json which define the Development environment
            // which require adding "ASPNETCORE_ENVIRONMENT": "Development" in computer environment variables
            webApplicationBuilder.Configuration.AddUserSecrets<Program>();
            webApplicationBuilder.Configuration.AddAzureAppConfiguration(options =>
            {
                var appConfigConnectionString = webApplicationBuilder.Configuration["AppConfig:ConnectionString"];

                options.Connect(appConfigConnectionString)
                    .Select("Acquisition:Api:*");
            });
            webApplicationBuilder.Services.AddAzureAppConfiguration();
        }
        // else use values define in appsettings.Development.json locally or appsettings.json on other environments
    }

    public static void UseConfiguration(this WebApplication webApplication)
    {
        if (_configurationType == ConfigurationType.AzureAppConfiguration)
            webApplication.UseAzureAppConfiguration();
    }

    public static void AddTelemetry(this WebApplicationBuilder webApplicationBuilder, bool activateTelemetry)
    {
        if (activateTelemetry)
        {
            var connectionString = webApplicationBuilder.Configuration["Acquisition:Api:Private:ApplicationInsights:ConnectionString"];
            webApplicationBuilder.Services.AddApplicationInsightsTelemetry(options => { options.ConnectionString = connectionString; });
        }
    }
}