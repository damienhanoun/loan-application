using Acquisition.Api.Domain.Entities;
using Acquisition.Api.Persistence.Database;
using AutomaticInterface;

namespace Acquisition.Api.Infrastructure.Persistence.ReadRepositories;

[GenerateAutomaticInterface]
public class ReadLoanContractRepository(AcquisitionContext acquisitionContext) : IReadLoanContractRepository
{
    public LoanContract GetLoanContract(Guid loanApplicationId)
    {
        var loanContract = acquisitionContext.LoanContracts
            .First(b => b.LoanApplicationId == loanApplicationId);
        return loanContract;
    }
}