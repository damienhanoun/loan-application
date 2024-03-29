using Acquisition.Domain.Entities;

namespace Acquisition.Application.Repositories;

public interface ILoanApplicationRepository
{
    Task CreateLoanApplication(LoanApplication loanApplication);
    LoanApplication GetLoanApplication(Guid loanApplicationId);
    Task UpdateLoanApplication(LoanApplication loanApplication);
}