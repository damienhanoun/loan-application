using Acquisition.Api.Scaffolding;
using Acquisition.Application.Requests;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Acquisition.Api.EndPoints;

public class EvaluateEligibility : IEndPoint
{
    public string Url => "evaluate-eligibility-to-a-loan";

    public Delegate Handler => async (IMediator mediator, [FromBody] EvaluateEligibilityToALoanQuery query) =>
    {
        var responseDto = await mediator.Send(query);
        return Results.Ok(responseDto);
    };
}