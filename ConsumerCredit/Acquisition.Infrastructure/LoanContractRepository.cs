using Acquisition.Application.Repositories;
using Acquisition.Domain.Entities;

namespace Acquisition.Infrastructure;

public class LoanContractRepository(AcquisitionContext acquisitionContext) : ILoanContractRepository
{
    public async Task Create(LoanContract loanContract)
    {
        acquisitionContext.LoanContracts.Add(loanContract);
        await acquisitionContext.SaveEntitiesAsync();
    }

    public LoanContract GetLoanContract(Guid loanApplicationId)
    {
        var loanContract = acquisitionContext.LoanContracts
            .First(b => b.LoanApplicationId == loanApplicationId);
        return loanContract;
    }

    public async Task UpdateLoanContract(LoanContract loanContract)
    {
        acquisitionContext.LoanContracts.Update(loanContract);
        await acquisitionContext.SaveEntitiesAsync();
    }
}