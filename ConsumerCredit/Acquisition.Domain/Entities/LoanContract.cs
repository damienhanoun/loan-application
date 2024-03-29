using Acquisition.Domain.DomainEvents;

namespace Acquisition.Domain.Entities;

public class LoanContract : Entity
{
    // Required by EF Core
    private LoanContract()
    {
    }

    private LoanContract(Guid id, Guid loanApplicationId)
    {
        Id = id;
        LoanApplicationId = loanApplicationId;
        AddDomainEvent(new LoanContractCreated(id, loanApplicationId));
    }

    public Guid LoanApplicationId { get; }
    public Guid Id { get; }

    public static LoanContract Create(Guid id, Guid loanApplicationId)
    {
        return new LoanContract(id, loanApplicationId);
    }
}