using System.Reflection;

namespace Acquisition.Api.Scaffolding;

public static class ServiceCollectionExtensions
{
    public static void AddServicesAndRepositories(this IServiceCollection services, Assembly assembly)
    {
        var types = assembly.GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false } &&
                        (t.Name.EndsWith("Repository") || t.Name.EndsWith("Service")));

        foreach (var type in types)
        {
            var interfaceType = type.GetInterfaces()[0];
            services.AddTransient(interfaceType, type);
        }
    }
}