using Acquisition.Application.Repositories;
using Acquisition.Domain.Entities;

namespace Acquisition.Infrastructure;

public class LoanApplicationRepository(AcquisitionContext acquisitionContext) : ILoanApplicationRepository
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