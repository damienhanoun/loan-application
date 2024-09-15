using Acquisition.Api.Domain.Entities;
using Acquisition.Api.Domain.ValueObjects;
using Acquisition.Api.Persistence.Database;
using Acquisition.Domain.ValueObjects;
using Microsoft.Extensions.DependencyInjection;

namespace Acquisition.Api.Tests.Helpers;

public class AcquisitionTestRepository(AcquisitionApiFactory acquisitionApiFactory)
{
    public async Task<Guid> CreateALoanApplication(decimal wishedAmount = 5000, string email = "email@email.fr")
    {
        using var scope = acquisitionApiFactory.Services.CreateScope();
        var acquisitionContext = scope.ServiceProvider.GetService<AcquisitionContext>()!;

        var loanApplicationId = Guid.NewGuid();
        var userInformation = new Dictionary<string, object> { { "Email", email } };
        var loanApplication = CreateLoanApplication(wishedAmount, userInformation, loanApplicationId);
        acquisitionContext.LoanApplications.Add(loanApplication);

        await acquisitionContext.SaveChangesAsync();
        return loanApplicationId;
    }

    public async Task<Guid> CreateALoanOffer()
    {
        using var scope = acquisitionApiFactory.Services.CreateScope();
        var acquisitionContext = scope.ServiceProvider.GetService<AcquisitionContext>()!;

        var loanOfferId = Guid.NewGuid();
        var loanOffer = new LoanOffer(loanOfferId, Amount.Create(1200), Maturity.Create(12), Amount.Create(100));
        acquisitionContext.LoanOffers.Add(loanOffer);

        await acquisitionContext.SaveChangesAsync();
        return loanOfferId;
    }

    public async Task<Guid> CreateALoanContract(Guid loanApplicationId, bool isSigned = false)
    {
        using var scope = acquisitionApiFactory.Services.CreateScope();
        var acquisitionContext = scope.ServiceProvider.GetService<AcquisitionContext>()!;

        var loanContractId = Guid.NewGuid();
        var loanContract = new LoanContract(loanContractId, loanApplicationId, isSigned);
        acquisitionContext.LoanContracts.Add(loanContract);

        await acquisitionContext.SaveChangesAsync();
        return loanContractId;
    }

    public LoanApplication GetLoanApplication(Guid loanApplicationId)
    {
        using var scope = acquisitionApiFactory.Services.CreateScope();
        var acquisitionContext = scope.ServiceProvider.GetService<AcquisitionContext>()!;
        var loanApplication = acquisitionContext.LoanApplications
            .First(b => b.Id == loanApplicationId);
        return loanApplication;
    }

    private static LoanApplication CreateLoanApplication(decimal wishedAmount, Dictionary<string, object> userInformation, Guid loanApplicationId)
    {
        var loanApplication = new LoanApplication(loanApplicationId);
        loanApplication.SetInitialLoanWish(new InitialLoanWish(Project.Create("a project"), Amount.Create(wishedAmount),
            Maturity.Create(12)));
        loanApplication.UpdateUserInformation(userInformation);
        return loanApplication;
    }
}