using Acquisition.Domain.ValueObjects;

namespace Acquisition.Domain.Entities;

public class LoanApplication
{
    // Required by EF Core
    private LoanApplication()
    {
    }

    public LoanApplication(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; private set; }
    public InitialLoanWish? InitialLoanWish { get; private set; }

    public void SetInitialLoanWish(InitialLoanWish initialLoanWish)
    {
        InitialLoanWish = initialLoanWish;
    }

    public bool EvaluateEligibility()
    {
        return true;
    }
}