using Acquisition.Api.Repositories;
using Acquisition.Domain.ValueObjects;
using AutomaticInterface;

namespace Acquisition.Api.Services;

[GenerateAutomaticInterface]
public class CommunicationService : ICommunicationService
{
    public Task SendPreAcceptationCommunication(Email userEmail)
    {
        return Task.CompletedTask;
    }
}