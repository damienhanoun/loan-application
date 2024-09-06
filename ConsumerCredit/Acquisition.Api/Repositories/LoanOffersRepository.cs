using Acquisition.Api.UseCases;
using AutomaticInterface;

namespace Acquisition.Api.Repositories;

[GenerateAutomaticInterface]
public class LoanOffersRepository : ILoanOffersRepository
{
    public List<LoanOfferDto> GetLoanOffers(decimal loanRequestedAmount)
    {
        return
        [
            new LoanOfferDto
            {
                Id = Guid.NewGuid(), Amount = loanRequestedAmount, Maturity = 12,
                MonthlyAmount = 850M
            },

            new LoanOfferDto
            {
                Id = Guid.NewGuid(), Amount = loanRequestedAmount, Maturity = 24,
                MonthlyAmount = 900M
            },

            new LoanOfferDto
            {
                Id = Guid.NewGuid(), Amount = loanRequestedAmount, Maturity = 36,
                MonthlyAmount = 950M
            }
        ];
    }
}