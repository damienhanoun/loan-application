using Acquisition.Api.Domain.DomainEvents;
using Acquisition.Api.Repositories;
using Mediator;

namespace Acquisition.Api.EventHandlers;

public class SendPreAcceptationEmailEventHandler(ICommunicationService communicationService, ILoanApplicationRepository loanApplicationRepository)
    : INotificationHandler<LoanContractCreated>
{
    public async ValueTask Handle(LoanContractCreated loanContractCreated, CancellationToken cancellationToken)
    {
        var loanApplication = loanApplicationRepository.GetLoanApplication(loanContractCreated.LoanApplicationId);
        await communicationService.SendPreAcceptationCommunication(loanApplication.UserInformation!.Email);
    }
}