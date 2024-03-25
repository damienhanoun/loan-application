using Acquisition.Api.Scaffolding;
using Acquisition.Application.LoanApplication.Queries;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Acquisition.Api.EndPoints.LoanOffers;

public class EvaluateEligibility : IEndPoint
{
    public string Url => "loan-offers/evaluate-eligibility-to-loan-offers";

    public Delegate Handler => async (IMediator mediator, [FromBody] EvaluateEligibilityToALoanQuery query) =>
    {
        var responseDto = await mediator.Send(query);
        return Results.Ok(responseDto);
    };
}