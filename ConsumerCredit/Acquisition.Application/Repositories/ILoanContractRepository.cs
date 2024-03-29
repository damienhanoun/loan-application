using Acquisition.Domain.Entities;

namespace Acquisition.Application.Repositories;

public interface ILoanContractRepository
{
    Task Create(LoanContract loanContract);
}