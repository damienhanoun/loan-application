using Acquisition.Api.Application.UseCases;
using AutomaticInterface;

namespace Acquisition.Api.Infrastructure.Persistence.ReadRepositories;

[GenerateAutomaticInterface]
public class ReadLoanOffersRepository : IReadLoanOffersRepository
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