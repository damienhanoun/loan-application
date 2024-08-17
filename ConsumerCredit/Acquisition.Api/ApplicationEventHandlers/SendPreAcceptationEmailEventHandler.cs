using Acquisition.Api.Domain.DomainEvents;
using Acquisition.Api.Repositories;
using Acquisition.Api.Services;
using Mediator;

namespace Acquisition.Api.ApplicationEventHandlers;

public class SendPreAcceptationEmailEventHandler(ICommunicationService communicationService, ILoanApplicationRepository loanApplicationRepository)
    : INotificationHandler<LoanContractCreated>
{
    public async ValueTask Handle(LoanContractCreated loanContractCreated, CancellationToken cancellationToken)
    {
        var loanApplication = loanApplicationRepository.GetLoanApplication(loanContractCreated.LoanApplicationId);
        await communicationService.SendPreAcceptationCommunication(loanApplication.UserInformation!.Email);
    }
}