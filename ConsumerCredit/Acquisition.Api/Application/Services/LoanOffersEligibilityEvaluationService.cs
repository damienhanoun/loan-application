﻿using Acquisition.Api.Domain.Entities;
using AutomaticInterface;

namespace Acquisition.Api.Application.Services;

[GenerateAutomaticInterface]
public class LoanOffersEligibilityEvaluationService : ILoanOffersEligibilityEvaluationService
{
    public bool EvaluateEligibilityToLoanOffers(LoanApplication loanApplication)
    {
        return true;
    }
}