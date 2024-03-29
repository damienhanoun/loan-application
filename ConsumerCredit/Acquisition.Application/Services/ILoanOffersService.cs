using Acquisition.Domain.Entities;

namespace Acquisition.Application.Services;

public interface ILoanOffersService
{
    List<LoanOffer> GetLoanOffers(Guid loanApplicationId);
}