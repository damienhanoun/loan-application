using Acquisition.Api.Domain.Entities;
using Acquisition.Api.Infrastructure.Persistence.Database;
using AutomaticInterface;

namespace Acquisition.Api.Infrastructure.Persistence.WriteRepositories;

[GenerateAutomaticInterface]
public class WriteLoanContractRepository(AcquisitionContext acquisitionContext) : IWriteLoanContractRepository
{
    public async Task Create(LoanContract loanContract)
    {
        acquisitionContext.LoanContracts.Add(loanContract);
        await acquisitionContext.SaveEntitiesAsync();
    }

    public async Task UpdateLoanContract(LoanContract loanContract)
    {
        acquisitionContext.LoanContracts.Update(loanContract);
        await acquisitionContext.SaveEntitiesAsync();
    }
}