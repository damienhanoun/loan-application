using Acquisition.Application.Repositories;
using Acquisition.Application.Services;
using Acquisition.Domain.DomainEvents;
using Mediator;

namespace Acquisition.Application.EventHandlers;

public class SendPreAcceptationEmailEventHandler(ICommunicationService communicationService, ILoanApplicationRepository loanApplicationRepository)
    : INotificationHandler<LoanContractCreated>
{
    public async ValueTask Handle(LoanContractCreated loanContractCreated, CancellationToken cancellationToken)
    {
        var loanApplication = loanApplicationRepository.GetLoanApplication(loanContractCreated.LoanApplicationId);
        await communicationService.SendPreAcceptationCommunication(loanApplication.UserInformation!.Email);
    }
}