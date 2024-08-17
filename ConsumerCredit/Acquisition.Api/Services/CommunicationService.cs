using Acquisition.Domain.ValueObjects;
using AutomaticInterface;

namespace Acquisition.Api.Repositories;

[GenerateAutomaticInterface]
public class CommunicationService : ICommunicationService
{
    public Task SendPreAcceptationCommunication(Email userEmail)
    {
        return Task.CompletedTask;
    }
}