using Acquisition.Api.Domain.Entities;
using Acquisition.Api.Persistence.Database;
using AutomaticInterface;

namespace Acquisition.Api.Infrastructure.Persistence.ReadRepositories;

[GenerateAutomaticInterface]
public class ReadLoanApplicationRepository(AcquisitionContext acquisitionContext) : IReadLoanApplicationRepository
{
    public LoanApplication GetLoanApplication(Guid loanApplicationId)
    {
        var loanApplication = acquisitionContext.LoanApplications
            .First(b => b.Id == loanApplicationId);
        return loanApplication;
    }
}