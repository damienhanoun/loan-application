using Mediator;

namespace Acquisition.Api.Domain.DomainEvents;

public record LoanOfferChosen(Guid LoanApplicationId, Guid LoanOfferId) : INotification;