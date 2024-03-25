using Acquisition.Api.Scaffolding;
using Acquisition.Application.LoanApplication.Commands;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Acquisition.Api.EndPoints.LoanApplications;

public class AddLoanWish : IEndPoint
{
    public string Url => "loan-applications/add-loan-wish";

    public Delegate Handler => async (IMediator mediator, [FromBody] AddLoanWishCommand addLoanWishCommand) =>
    {
        await mediator.Send(addLoanWishCommand);
        return Results.Ok();
    };
}