namespace Acquisition.Api.Scaffolding;

public static class ClassDiscovery
{
    public static IEnumerable<T> DiscoverAll<T>()
    {
        return typeof(T).Assembly
            .GetTypes()
            .Where(p => p.IsClass && p.IsAssignableTo(typeof(T)))
            .Select(Activator.CreateInstance)
            .Cast<T>();
    }
}