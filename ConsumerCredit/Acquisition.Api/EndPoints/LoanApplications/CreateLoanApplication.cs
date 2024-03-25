using Acquisition.Api.Scaffolding;
using Acquisition.Application.LoanApplication.Commands;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Acquisition.Api.EndPoints.LoanApplications;

public class CreateLoanApplication : IEndPoint
{
    public string Url => "loan-applications/create";

    public Delegate Handler => async (IMediator mediator, [FromBody] CreateLoanApplicationCommand createLoanApplicationCommand) =>
    {
        var responseDto = await mediator.Send(createLoanApplicationCommand);
        return Results.Created("loan-applications/create", responseDto);
    };
}