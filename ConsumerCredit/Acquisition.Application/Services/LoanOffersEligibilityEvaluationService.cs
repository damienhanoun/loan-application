using Acquisition.Domain.Entities;

namespace Acquisition.Application.Services;

public class LoanOffersEligibilityEvaluationService : ILoanOffersEligibilityEvaluationService
{
    public bool EvaluateEligibilityToLoanOffers(LoanApplication loanApplication)
    {
        return true;
    }
}