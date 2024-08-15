using Acquisition.Application.Repositories;
using FastEndpoints;

namespace Acquisition.Api.EndPoints;

public record SaveUserInformationCommand(Guid LoanApplicationId, string Email);

public class SaveUserInformation(ILoanApplicationRepository loanApplicationRepository) : Endpoint<SaveUserInformationCommand>
{
    public override void Configure()
    {
        Post("/save-user-information");
        AllowAnonymous();
    }

    public override async Task HandleAsync(SaveUserInformationCommand request, CancellationToken ct)
    {
        var loanApplication = loanApplicationRepository.GetLoanApplication(request.LoanApplicationId);
        loanApplication.SaveUserInformation(request.Email);
        await loanApplicationRepository.UpdateLoanApplication(loanApplication);
        await SendOkAsync(ct);
    }
}