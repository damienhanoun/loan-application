using Acquisition.Application.Repositories;
using Acquisition.Domain.Entities;
using Acquisition.Domain.ValueObjects;

namespace Acquisition.Application.Services;

public class LoanOffersService(ILoanApplicationRepository loanApplicationRepository) : ILoanOffersService
{
    public List<LoanOffer> GetLoanOffers(Guid loanApplicationId)
    {
        var loanApplication = loanApplicationRepository.GetLoanApplication(loanApplicationId);
        return [LoanOffer.Create(Guid.NewGuid(), loanApplication.InitialLoanWish!.Amount, Maturity.Create(12)).Value!];
    }
}