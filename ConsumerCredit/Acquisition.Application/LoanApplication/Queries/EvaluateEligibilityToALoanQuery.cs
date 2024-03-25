using Acquisition.Application.LoanApplication.Dtos;
using Mediator;

namespace Acquisition.Application.LoanApplication.Queries;

public record EvaluateEligibilityToALoanQuery(Guid LoanApplicationId) : IQuery<EvaluateEligibilityToALoanResponseDto>;

public class EvaluateEligibilityToALoanHandler : IQueryHandler<EvaluateEligibilityToALoanQuery, EvaluateEligibilityToALoanResponseDto>
{
    private readonly ILoanApplicationRepository _loanApplicationRepository;

    public EvaluateEligibilityToALoanHandler(ILoanApplicationRepository loanApplicationRepository)
    {
        _loanApplicationRepository = loanApplicationRepository;
    }

    public ValueTask<EvaluateEligibilityToALoanResponseDto> Handle(EvaluateEligibilityToALoanQuery query, CancellationToken cancellationToken)
    {
        var loanApplication = _loanApplicationRepository.GetLoanApplication(query.LoanApplicationId);
        var isEligibleToALoan = loanApplication.EvaluateEligibility();
        return ValueTask.FromResult(new EvaluateEligibilityToALoanResponseDto { IsEligibileToALoan = isEligibleToALoan });
    }
}