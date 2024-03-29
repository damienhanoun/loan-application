using Acquisition.Domain.ValueObjects;

namespace Acquisition.Domain.Entities;

public class LoanOffer : Entity
{
    // Required by EF Core
    private LoanOffer()
    {
    }

    public LoanOffer(Guid id, Amount amount, Maturity maturity, Amount monthlyAmount)
    {
        Id = id;
        Amount = amount;
        Maturity = maturity;
        MonthlyAmount = monthlyAmount;
    }

    public Guid Id { get; private set; }
    public Amount Amount { get; private set; } = null!;
    public Maturity Maturity { get; private set; } = null!;
    public Amount MonthlyAmount { get; private set; } = null!;
}