namespace Acquisition.Application.Dtos;

public class LoanOfferDto
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public int Maturity { get; set; }
    public decimal MonthlyAmount { get; set; }
}