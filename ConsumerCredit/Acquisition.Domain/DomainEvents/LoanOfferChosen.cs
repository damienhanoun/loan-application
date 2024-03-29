using Mediator;

namespace Acquisition.Domain.DomainEvents;

public record LoanOfferChosen(Guid LoanApplicationId, Guid LoanOfferId) : INotification;