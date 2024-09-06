using Acquisition.Api.Domain.Entities;
using Acquisition.Api.Persistence.Database;
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

    public LoanApplication GetLoanApplication(Guid loanApplicationId)
    {
        var loanApplication = acquisitionContext.LoanApplications
            .First(b => b.Id == loanApplicationId);
        return loanApplication;
    }

    public async Task UpdateLoanApplication(LoanApplication loanApplication)
    {
        acquisitionContext.LoanApplications.Update(loanApplication);
        await acquisitionContext.SaveEntitiesAsync();
    }
}