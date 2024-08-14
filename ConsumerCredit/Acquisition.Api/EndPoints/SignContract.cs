using Acquisition.Application.Requests;
using FastEndpoints;
using Mediator;

namespace Acquisition.Api.EndPoints;

public class SignContract(IMediator mediator) : Endpoint<SignContractCommand>
{
    public override void Configure()
    {
        Post("/sign-contract");
        AllowAnonymous();
    }

    public override async Task HandleAsync(SignContractCommand command, CancellationToken ct)
    {
        await mediator.Send(command, ct);
        await SendOkAsync(ct);
    }
}