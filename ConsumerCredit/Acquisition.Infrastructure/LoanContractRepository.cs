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
}