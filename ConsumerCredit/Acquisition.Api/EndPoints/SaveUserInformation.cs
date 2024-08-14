using Acquisition.Application.Requests;
using FastEndpoints;
using Mediator;

namespace Acquisition.Api.EndPoints;

public class SaveUserInformation(IMediator mediator) : Endpoint<SaveUserInformationCommand>
{
    public override void Configure()
    {
        Post("/save-user-information");
        AllowAnonymous();
    }

    public override async Task HandleAsync(SaveUserInformationCommand command, CancellationToken ct)
    {
        await mediator.Send(command, ct);
        await SendOkAsync(ct);
    }
}