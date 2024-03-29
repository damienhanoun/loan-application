using Acquisition.Application.Services;
using Acquisition.Domain.ValueObjects;

namespace Acquisition.Infrastructure;

public class CommunicationService : ICommunicationService
{
    public Task SendPreAcceptationCommunication(Email userEmail)
    {
        return Task.CompletedTask;
    }
}