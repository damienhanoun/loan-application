using Mediator;

namespace Acquisition.Api.Domain.Events;

public record LoanOfferChosen(Guid LoanApplicationId, Guid LoanOfferId) : INotification;