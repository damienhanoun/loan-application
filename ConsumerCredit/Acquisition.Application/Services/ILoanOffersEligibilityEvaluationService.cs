using Acquisition.Domain.Entities;

namespace Acquisition.Application.Services;

public interface ILoanOffersEligibilityEvaluationService
{
    bool EvaluateEligibilityToLoanOffers(LoanApplication loanApplication);
}