using Acquisition.Domain.Entities;
using Acquisition.Domain.ValueObjects;

namespace Acquisition.Application.Services;

public class LoanOffersService : ILoanOffersService
{
    public List<LoanOffer> GetLoanOffers(Guid loanApplicationId)
    {
        return new List<LoanOffer>
        {
            new(Guid.NewGuid(), Amount.Create(1000), Maturity.Create(12), Amount.Create(1000 / 12))
        };
    }
}