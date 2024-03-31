using Acquisition.Api.Scaffolding;
using Acquisition.Application.Requests;
using Mediator;

namespace Acquisition.Api.EndPoints;

public class SignContract : IEndPoint
{
    public string Url => "sign-contract";

    public Delegate Handler => async (IMediator mediator, SignContractCommand command) =>
    {
        await mediator.Send(command);
        return Results.Ok();
    };
}