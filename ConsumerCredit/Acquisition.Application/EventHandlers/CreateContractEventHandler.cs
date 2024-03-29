using Acquisition.Application.Repositories;
using Acquisition.Domain.DomainEvents;
using Acquisition.Domain.Entities;
using Mediator;

namespace Acquisition.Application.EventHandlers;

public class CreateContractEventHandler(ILoanContractRepository loanContractRepository) : INotificationHandler<LoanOfferChosen>
{
    public async ValueTask Handle(LoanOfferChosen notification, CancellationToken cancellationToken)
    {
        var loanContract = LoanContract.Create(Guid.NewGuid(), notification.LoanApplicationId);
        await loanContractRepository.Create(loanContract);
    }
}