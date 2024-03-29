using Acquisition.Api.Scaffolding;
using Acquisition.Application.Requests;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Acquisition.Api.EndPoints;

public class ChooseALoanOffer : IEndPoint
{
    public string Url => "choose-a-loan-offer";

    public Delegate Handler => async (IMediator mediator, [FromBody] ChooseALoanOfferCommand command) =>
    {
        await mediator.Send(command);
        return Results.Ok();
    };
}