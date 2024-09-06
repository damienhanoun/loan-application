using Acquisition.Api.Infrastructure.Persistence.ReadRepositories;
using Acquisition.Api.Infrastructure.Persistence.WriteRepositories;
using FastEndpoints;

namespace Acquisition.Api.Application.UseCases;

public class SaveUserInformation(
    IReadLoanApplicationRepository readLoanApplicationRepository,
    IWriteLoanApplicationRepository writeLoanApplicationRepository)
    : Endpoint<SaveUserInformationCommand>
{
    public override void Configure()
    {
        Post("/save-user-information");
        AllowAnonymous();
    }

    public override async Task HandleAsync(SaveUserInformationCommand request, CancellationToken ct)
    {
        var loanApplication = readLoanApplicationRepository.GetLoanApplication(request.LoanApplicationId);
        loanApplication.SaveUserInformation(request.Email);
        await writeLoanApplicationRepository.UpdateLoanApplication(loanApplication);
        await SendOkAsync(ct);
    }
}

public record SaveUserInformationCommand(Guid LoanApplicationId, string Email);