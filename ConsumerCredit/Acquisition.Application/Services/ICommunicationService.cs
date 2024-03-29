using Acquisition.Domain.ValueObjects;

namespace Acquisition.Application.Services;

public interface ICommunicationService
{
    Task SendPreAcceptationCommunication(Email userEmail);
}