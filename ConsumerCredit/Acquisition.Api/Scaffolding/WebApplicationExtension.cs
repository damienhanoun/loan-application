using static Acquisition.Api.Scaffolding.ClassDiscovery;

namespace Acquisition.Api.Scaffolding;

public static class WebApplicationExtension
{
    public static IEndpointRouteBuilder MapEndpoints(this WebApplication app)
    {
        var endPoints = DiscoverAll<IEndPoint>();
        foreach (var newEndPoint in endPoints) app.MapPost($"{newEndPoint.Url}", newEndPoint.Handler);

        return app;
    }
}