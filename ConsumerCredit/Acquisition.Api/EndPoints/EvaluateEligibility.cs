using Acquisition.Application.Repositories;
using Acquisition.Application.Services;
using FastEndpoints;

namespace Acquisition.Api.EndPoints;

public class EvaluateEligibility(
    ILoanApplicationRepository loanApplicationRepository,
    ILoanOffersEligibilityEvaluationService loanOffersEligibilityEvaluationService)
    : Endpoint<EvaluateEligibilityToALoanQuery, EvaluateEligibilityToALoanResponseDto>
{
    public override void Configure()
    {
        Post("/evaluate-eligibility-to-a-loan");
        AllowAnonymous();
    }

    public override async Task HandleAsync(EvaluateEligibilityToALoanQuery request, CancellationToken ct)
    {
        var loanApplication = loanApplicationRepository.GetLoanApplication(request.LoanApplicationId);
        var isEligibleToALoan = loanOffersEligibilityEvaluationService.EvaluateEligibilityToLoanOffers(loanApplication);
        var responseDto = new EvaluateEligibilityToALoanResponseDto(isEligibleToALoan);
        await SendOkAsync(responseDto, ct);
    }
}

public record EvaluateEligibilityToALoanQuery(Guid LoanApplicationId);

public record EvaluateEligibilityToALoanResponseDto(bool IsEligibleToALoan);