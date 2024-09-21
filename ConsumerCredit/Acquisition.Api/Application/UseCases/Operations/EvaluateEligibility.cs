using Acquisition.Api.Application.Services;
using Acquisition.Api.Infrastructure.Persistence.ReadRepositories;
using FastEndpoints;

namespace Acquisition.Api.Application.UseCases.Operations;

public class EvaluateEligibility(
    IReadLoanApplicationRepository readLoanApplicationRepository,
    ILoanOffersEligibilityEvaluationService loanOffersEligibilityEvaluationService)
    : Endpoint<EvaluateEligibilityToALoanQuery, EvaluateEligibilityToALoanResponseDto>
{
    public override void Configure()
    {
        Post("/evaluate-loan-eligibility");
        AllowAnonymous();
        Description(x => x
            .WithTags("Evaluate eligibility")
            .Produces(200, typeof(EvaluateEligibilityToALoanResponseDto), "application/json")
            .ProducesProblem(500));
    }

    public override async Task HandleAsync(EvaluateEligibilityToALoanQuery request, CancellationToken ct)
    {
        var loanApplication = readLoanApplicationRepository.GetLoanApplication(request.LoanApplicationId);
        var isEligibleToALoan = loanOffersEligibilityEvaluationService.EvaluateEligibilityToLoanOffers(loanApplication);
        var responseDto = new EvaluateEligibilityToALoanResponseDto(isEligibleToALoan);
        await SendOkAsync(responseDto, ct);
    }
}

public record EvaluateEligibilityToALoanQuery(Guid LoanApplicationId);

public record EvaluateEligibilityToALoanResponseDto(bool IsEligibleToALoan);