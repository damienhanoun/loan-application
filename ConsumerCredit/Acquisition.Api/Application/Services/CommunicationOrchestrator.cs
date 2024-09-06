using Acquisition.Api.Infrastructure.Services;
using Acquisition.Domain.ValueObjects;
using AutomaticInterface;

namespace Acquisition.Api.Application.Services;

[GenerateAutomaticInterface]
public class CommunicationOrchestrator(ICommunicationService communicationService) : ICommunicationOrchestrator
{
    public Task SendPreAcceptationCommunication(Email userEmail)
    {
        return communicationService.SendEmail(userEmail, "you are preaccepted");
    }
}