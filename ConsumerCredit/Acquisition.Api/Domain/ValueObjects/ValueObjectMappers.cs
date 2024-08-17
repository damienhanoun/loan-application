using Acquisition.Domain.ValueObjects;

namespace Acquisition.Api.Domain.ValueObjects;

public static class ValueObjectMappers
{
    public static decimal MapAmount(Amount amount)
    {
        return amount.Value;
    }

    public static int MapMaturity(Maturity maturity)
    {
        return maturity.Value;
    }

    public static string MapProject(Project project)
    {
        return project.Value;
    }

    public static Project CreateProject(string project)
    {
        return Project.Create(project);
    }

    public static Amount CreateAmount(decimal amount)
    {
        return Amount.Create(amount);
    }

    public static Maturity CreateMaturity(int maturity)
    {
        return Maturity.Create(maturity);
    }
}