using Acquisition.Domain.DomainEvents;
using Acquisition.Domain.ValueObjects;

namespace Acquisition.Domain.Entities;

public class LoanApplication : Entity
{
    // Required by EF Core
    private LoanApplication()
    {
    }

    public LoanApplication(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
    public InitialLoanWish? InitialLoanWish { get; private set; }
    public Guid? LoanOfferId { get; private set; }
    public UserInformation? UserInformation { get; }

    public void SetInitialLoanWish(InitialLoanWish initialLoanWish)
    {
        InitialLoanWish = initialLoanWish;
    }

    public void ChooseALoanOffer(Guid loanOfferId)
    {
        LoanOfferId = loanOfferId;
        AddDomainEvent(new LoanOfferChosen(Id, loanOfferId));
    }
}