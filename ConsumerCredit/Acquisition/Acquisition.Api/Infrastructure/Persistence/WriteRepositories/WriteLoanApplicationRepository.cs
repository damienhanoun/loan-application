using Acquisition.Api.Domain.Entities;
using Acquisition.Api.Infrastructure.Persistence.Database;
using AutomaticInterface;

namespace Acquisition.Api.Infrastructure.Persistence.WriteRepositories;

[GenerateAutomaticInterface]
public class WriteLoanApplicationRepository(AcquisitionContext acquisitionContext) : IWriteLoanApplicationRepository
{
    public async Task CreateLoanApplication(LoanApplication loanApplication)
    {
        acquisitionContext.LoanApplications.Add(loanApplication);
        await acquisitionContext.SaveEntitiesAsync();
    }

    public async Task UpdateLoanApplication(LoanApplication loanApplication)
    {
        acquisitionContext.LoanApplications.Update(loanApplication);
        await acquisitionContext.SaveEntitiesAsync();
    }
}