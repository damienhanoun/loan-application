using System.Reflection;

namespace Acquisition.Api.Scaffolding;

public static class ServiceCollectionExtensions
{
    private static readonly string[] TypeSuffixes = { "Repository", "Service" };

    public static void AddServicesAndRepositories(this IServiceCollection services, Assembly assembly)
    {
        var types = assembly.GetTypes().Where(IsRelevantType);

        foreach (var type in types)
        {
            var interfaceType = type.GetInterfaces()[0];
            services.AddTransient(interfaceType, type);
        }
    }

    private static bool IsRelevantType(Type type)
    {
        return type is { IsClass: true, IsAbstract: false } &&
               Array.Exists(TypeSuffixes, suffix => type.Name.EndsWith(suffix));
    }
}