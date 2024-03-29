using Acquisition.Application.Dtos;
using Acquisition.Application.Repositories;
using Acquisition.Application.Services;
using Mediator;

namespace Acquisition.Application.Requests;

public record EvaluateEligibilityToALoanQuery(Guid LoanApplicationId) : IQuery<EvaluateEligibilityToALoanResponseDto>;

public class EvaluateEligibilityToALoanHandler(
    ILoanApplicationRepository loanApplicationRepository,
    ILoanOffersEligibilityEvaluationService loanOffersEligibilityEvaluationService)
    : IQueryHandler<EvaluateEligibilityToALoanQuery, EvaluateEligibilityToALoanResponseDto>
{
    public ValueTask<EvaluateEligibilityToALoanResponseDto> Handle(EvaluateEligibilityToALoanQuery query, CancellationToken cancellationToken)
    {
        var loanApplication = loanApplicationRepository.GetLoanApplication(query.LoanApplicationId);
        var isEligibleToALoan = loanOffersEligibilityEvaluationService.EvaluateEligibilityToLoanOffers(loanApplication);
        return ValueTask.FromResult(new EvaluateEligibilityToALoanResponseDto { IsEligibleToALoan = isEligibleToALoan });
    }
}