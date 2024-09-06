using Mediator;

namespace Acquisition.Api.Domain.Events;

public record LoanContractCreated(Guid LoanContractId, Guid LoanApplicationId) : INotification;