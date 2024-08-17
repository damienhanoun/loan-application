using Acquisition.Domain.Entities;
using AutomaticInterface;

namespace Acquisition.Api.Services;

[GenerateAutomaticInterface]
public class LoanOffersEligibilityEvaluationService : ILoanOffersEligibilityEvaluationService
{
    public bool EvaluateEligibilityToLoanOffers(LoanApplication loanApplication)
    {
        return true;
    }
}