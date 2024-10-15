using Acquisition.Api.Client;
using FastEndpoints;

namespace Acquisition.WebApplication.Server.Application.UseCases.Operations;

public class EvaluateLoanEligibility(IAcquisitionApiClient acquisitionClient)
    : Endpoint<EvaluateEligibilityToALoanQuery, EvaluateEligibilityToALoanResponseDto>
{
    public override void Configure()
    {
        Post("/evaluate-loan-eligibility");
        AllowAnonymous();
        Description(x => x
            .WithTags("Operations")
            .Produces(200, typeof(EvaluateEligibilityToALoanResponseDto), "application/json")
            .ProducesProblem(500));
    }

    public override async Task HandleAsync(EvaluateEligibilityToALoanQuery query, CancellationToken ct)
    {
        var responseDto = await acquisitionClient.EvaluateLoanEligibilityAsync(query, ct);
        await SendOkAsync(responseDto, ct);
    }
}