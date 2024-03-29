namespace Acquisition.Application.Dtos;

public class GetLoanOffersResponseDto
{
    public IEnumerable<LoanOfferDto> LoanOffers { get; set; } = new List<LoanOfferDto>();
}