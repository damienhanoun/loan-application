using Acquisition.Api.Application.Services;
using Acquisition.Api.Infrastructure.Persistence.ReadRepositories;
using FastEndpoints;

namespace Acquisition.Api.Application.UseCases.Entities.LoanOffers;

public class EvaluateLoanEligibility(
    IReadLoanApplicationRepository readLoanApplicationRepository,
    ILoanOffersEligibilityEvaluationService loanOffersEligibilityEvaluationService)
    : Endpoint<EvaluateEligibilityToALoanCommand, EvaluateEligibilityToALoanResponseDto>
{
    public override void Configure()
    {
        Post("/evaluate-loan-eligibility");
        AllowAnonymous();
        Description(x => x
            .WithTags("Loan offer")
            .Produces(200, typeof(EvaluateEligibilityToALoanResponseDto), "application/json")
            .ProducesProblem(500));
    }

    public override async Task HandleAsync(EvaluateEligibilityToALoanCommand request, CancellationToken ct)
    {
        var loanApplication = readLoanApplicationRepository.GetLoanApplication(request.LoanApplicationId);
        var isEligibleToALoan = loanOffersEligibilityEvaluationService.EvaluateEligibilityToLoanOffers(loanApplication);
        var responseDto = new EvaluateEligibilityToALoanResponseDto(isEligibleToALoan);
        await SendOkAsync(responseDto, ct);
    }
}

public record EvaluateEligibilityToALoanCommand(Guid LoanApplicationId);

public record EvaluateEligibilityToALoanResponseDto(bool IsEligibleToALoan);