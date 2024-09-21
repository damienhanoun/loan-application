using Acquisition.Api.Application.UseCases;
using Acquisition.Api.Application.UseCases.LoanOffers;
using AutomaticInterface;

namespace Acquisition.Api.Infrastructure.Persistence.WriteRepositories;

[GenerateAutomaticInterface]
public class WriteLoanOffersRepository : IWriteLoanOffersRepository
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