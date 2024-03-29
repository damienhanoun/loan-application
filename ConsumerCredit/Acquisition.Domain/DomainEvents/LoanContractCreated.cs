using Mediator;

namespace Acquisition.Domain.DomainEvents;

public record LoanContractCreated(Guid LoanContractId, Guid LoanApplicationId) : INotification;