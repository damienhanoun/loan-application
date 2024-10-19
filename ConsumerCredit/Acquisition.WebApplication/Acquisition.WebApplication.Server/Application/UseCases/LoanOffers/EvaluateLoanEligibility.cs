using Acquisition.Api.Client;
using FastEndpoints;

namespace Acquisition.WebApplication.Server.Application.UseCases.LoanOffers;

public class EvaluateLoanEligibility(IAcquisitionApiClient acquisitionClient)
    : Endpoint<EvaluateEligibilityToALoanCommand, EvaluateEligibilityToALoanResponseDto>
{
    public override void Configure()
    {
        Post("/evaluate-loan-eligibility");
        AllowAnonymous();
        Description(x => x
            .WithTags("LoanOffers")
            .Produces(200, typeof(EvaluateEligibilityToALoanResponseDto), "application/json")
            .ProducesProblem(500));
    }

    public override async Task HandleAsync(EvaluateEligibilityToALoanCommand request, CancellationToken ct)
    {
        var responseDto = await acquisitionClient.EvaluateLoanEligibilityAsync(request, ct);
        await SendOkAsync(responseDto, ct);
    }
}