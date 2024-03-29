using Acquisition.Api.Scaffolding;
using Acquisition.Application.Requests;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Acquisition.Api.EndPoints;

public class ExpressLoanWish : IEndPoint
{
    public string Url => "express-loan-wish";

    public Delegate Handler => async (IMediator mediator, [FromBody] ExpressLoanWishCommand addLoanWishCommand) =>
    {
        var responseDto = await mediator.Send(addLoanWishCommand);
        return Results.Ok(responseDto);
    };
}