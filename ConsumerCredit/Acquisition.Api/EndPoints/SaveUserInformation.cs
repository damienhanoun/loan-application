using Acquisition.Api.Scaffolding;
using Acquisition.Application.Requests;
using Mediator;

namespace Acquisition.Api.EndPoints;

public class SaveUserInformation : IEndPoint
{
    public string Url => "save-user-information";

    public Delegate Handler => async (IMediator mediator, SaveUserInformationCommand command) =>
    {
        await mediator.Send(command);
        return Results.Ok();
    };
}