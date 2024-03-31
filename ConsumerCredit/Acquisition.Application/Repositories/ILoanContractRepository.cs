using Acquisition.Domain.Entities;

namespace Acquisition.Application.Repositories;

public interface ILoanContractRepository
{
    Task Create(LoanContract loanContract);
    LoanContract GetLoanContract(Guid loanApplicationId);
    Task UpdateLoanContract(LoanContract loanContract);
}