using Acquisition.Api.Application.Services;
using Acquisition.Api.Domain.Events;
using Acquisition.Api.Infrastructure.Persistence.ReadRepositories;
using Mediator;

namespace Acquisition.Api.Application.EventHandlers;

public class SendPreAcceptationEmailEventHandler(
    ILoanApplicationCommunicationService loanApplicationCommunicationService,
    IReadLoanApplicationRepository readLoanApplicationRepository)
    : INotificationHandler<LoanContractCreated>
{
    public async ValueTask Handle(LoanContractCreated loanContractCreated, CancellationToken cancellationToken)
    {
        var loanApplication = readLoanApplicationRepository.GetLoanApplication(loanContractCreated.LoanApplicationId);
        await loanApplicationCommunicationService.SendPreAcceptationCommunication(loanApplication.UserInformation!.Email);
    }
}