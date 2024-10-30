using Acquisition.Api.Infrastructure.Services;
using Acquisition.Domain.ValueObjects;
using AutomaticInterface;

namespace Acquisition.Api.Application.Services;

[GenerateAutomaticInterface]
public class LoanApplicationCommunicationService(ICommunicationService communicationService) : ILoanApplicationCommunicationService
{
    public Task SendPreAcceptationCommunication(Email userEmail)
    {
        return communicationService.SendEmail(userEmail, "you are preaccepted");
    }
}