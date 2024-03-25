using Acquisition.Application.LoanApplication;
using Acquisition.Domain.Entities;

namespace Acquisition.Infrastructure;

public class LoanApplicationRepository : ILoanApplicationRepository
{
    private readonly AcquisitionContext _acquisitionContext;

    public LoanApplicationRepository(AcquisitionContext acquisitionContext)
    {
        _acquisitionContext = acquisitionContext;
    }

    public void CreateLoanApplication(LoanApplication loanApplication)
    {
        _acquisitionContext.LoanApplications.Add(loanApplication);
        _acquisitionContext.SaveChanges();
    }

    public LoanApplication GetLoanApplication(Guid loanApplicationId)
    {
        var loanApplication = _acquisitionContext.LoanApplications
            .First(b => b.Id == loanApplicationId);
        return loanApplication;
    }

    public void UpdateLoanApplication(LoanApplication loanApplication)
    {
        _acquisitionContext.LoanApplications.Update(loanApplication);
        _acquisitionContext.SaveChanges();
    }
}