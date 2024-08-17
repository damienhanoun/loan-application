using Acquisition.Api.Domain.DomainEvents;
using Acquisition.Api.Domain.Entities;
using Acquisition.Api.Repositories;
using Mediator;

namespace Acquisition.Api.Domain.DomainEventHandlers;

public class CreateContractEventHandler(ILoanContractRepository loanContractRepository) : INotificationHandler<LoanOfferChosen>
{
    public async ValueTask Handle(LoanOfferChosen notification, CancellationToken cancellationToken)
    {
        var loanContract = LoanContract.Create(Guid.NewGuid(), notification.LoanApplicationId);
        await loanContractRepository.Create(loanContract);
    }
}