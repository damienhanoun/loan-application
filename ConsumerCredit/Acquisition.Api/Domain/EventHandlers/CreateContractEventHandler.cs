using Acquisition.Api.Domain.DomainEvents;
using Acquisition.Api.Domain.Entities;
using Acquisition.Api.Infrastructure.Persistence.WriteRepositories;
using Mediator;

namespace Acquisition.Api.Domain.EventHandlers;

public class CreateContractEventHandler(IWriteLoanContractRepository writeLoanContractRepository)
    : INotificationHandler<LoanOfferChosen>
{
    public async ValueTask Handle(LoanOfferChosen notification, CancellationToken cancellationToken)
    {
        var loanContract = LoanContract.Create(Guid.NewGuid(), notification.LoanApplicationId);
        await writeLoanContractRepository.Create(loanContract);
    }
}