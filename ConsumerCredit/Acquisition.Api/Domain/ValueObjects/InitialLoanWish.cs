using Acquisition.Domain.ValueObjects;

namespace Acquisition.Api.Domain.ValueObjects;

public class InitialLoanWish
{
    // Required by EF Core
    private InitialLoanWish()
    {
    }

    public InitialLoanWish(Project project, Amount amount, Maturity maturity)
    {
        Project = project;
        Amount = amount;
        Maturity = maturity;
    }

    public Project Project { get; } = null!;
    public Amount Amount { get; } = null!;
    public Maturity Maturity { get; } = null!;
}