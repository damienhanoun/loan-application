using Acquisition.Domain.ValueObjects;
using AutomaticInterface;

namespace Acquisition.Api.Infrastructure.Services;

[GenerateAutomaticInterface]
public class CommunicationService : ICommunicationService
{
    public Task SendEmail(Email userEmail, string content)
    {
        return Task.CompletedTask;
    }
}