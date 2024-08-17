using Mediator;

namespace Acquisition.Api.Domain.DomainEvents;

public record LoanContractCreated(Guid LoanContractId, Guid LoanApplicationId) : INotification;