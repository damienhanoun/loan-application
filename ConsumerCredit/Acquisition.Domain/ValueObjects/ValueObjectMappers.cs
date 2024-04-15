using Riok.Mapperly.Abstractions;

namespace Acquisition.Domain.ValueObjects;

[Mapper]
public static partial class ValueObjectMappers
{
    [ObjectFactory]
    public static decimal CreateAmount(Amount amount)
    {
        return amount.Value;
    }

    [ObjectFactory]
    public static int CreateMaturity(Maturity maturity)
    {
        return maturity.Value;
    }

    [ObjectFactory]
    public static string CreateProject(Project project)
    {
        return project.Value;
    }

    [ObjectFactory]
    public static Project CreateProject(string project)
    {
        return Project.Create(project);
    }

    [ObjectFactory]
    public static Amount CreateAmount(decimal amount)
    {
        return Amount.Create(amount);
    }

    [ObjectFactory]
    public static Maturity CreateMaturity(int maturity)
    {
        return Maturity.Create(maturity);
    }
}