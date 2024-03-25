namespace Acquisition.Application.LoanApplication;

public interface ILoanApplicationRepository
{
    void CreateLoanApplication(Domain.Entities.LoanApplication loanApplication);
    Domain.Entities.LoanApplication GetLoanApplication(Guid loanApplicationId);
    void UpdateLoanApplication(Domain.Entities.LoanApplication loanApplication);
}