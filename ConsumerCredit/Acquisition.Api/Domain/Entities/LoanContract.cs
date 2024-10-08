﻿using Acquisition.Api.Domain.Events;

namespace Acquisition.Api.Domain.Entities;

public class LoanContract : Entity
{
    // Required by EF Core
    private LoanContract()
    {
    }

    public LoanContract(Guid id, Guid loanApplicationId, bool isSigned)
    {
        Id = id;
        LoanApplicationId = loanApplicationId;
        IsSigned = isSigned;
        AddDomainEvent(new LoanContractCreated(id, loanApplicationId));
    }

    public Guid LoanApplicationId { get; }
    public Guid Id { get; }
    public bool IsSigned { get; private set; }

    public static LoanContract Create(Guid id, Guid loanApplicationId, bool isSigned = false)
    {
        return new LoanContract(id, loanApplicationId, isSigned);
    }

    public void SignContract()
    {
        IsSigned = true;
    }
}