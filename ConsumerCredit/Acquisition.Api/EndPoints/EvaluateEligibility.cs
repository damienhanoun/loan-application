using Acquisition.Application.Dtos;
using Acquisition.Application.Requests;
using FastEndpoints;
using Mediator;

namespace Acquisition.Api.EndPoints;

public class EvaluateEligibility(IMediator mediator) : Endpoint<EvaluateEligibilityToALoanQuery, EvaluateEligibilityToALoanResponseDto>
{
    public override void Configure()
    {
        Post("/evaluate-eligibility-to-a-loan");
        AllowAnonymous();
    }

    public override async Task HandleAsync(EvaluateEligibilityToALoanQuery query, CancellationToken ct)
    {
        var responseDto = await mediator.Send(query, ct);
        await SendOkAsync(responseDto, ct);
    }
}