using Acquisition.Api.Infrastructure.Persistence.ReadRepositories;
using Acquisition.Api.Infrastructure.Persistence.WriteRepositories;
using FastEndpoints;

namespace Acquisition.Api.Application.UseCases.Entities.LoanApplications;

public class UpdateUserInformation(
    IReadLoanApplicationRepository readLoanApplicationRepository,
    IWriteLoanApplicationRepository writeLoanApplicationRepository)
    : Endpoint<UpdateUserInformationCommand>
{
    public override void Configure()
    {
        Post("/update-user-information");
        AllowAnonymous();
        Description(x => x
            .WithTags("Update user information")
            .Produces(200, typeof(void), "application/json")
            .ProducesProblem(500));
    }

    public override async Task HandleAsync(UpdateUserInformationCommand request, CancellationToken ct)
    {
        var loanApplication = readLoanApplicationRepository.GetLoanApplication(request.LoanApplicationId);
        loanApplication.UpdateUserInformation(request.UpdatedProperties);
        await writeLoanApplicationRepository.UpdateLoanApplication(loanApplication);
        await SendOkAsync(ct);
    }
}

public record UpdateUserInformationCommand(Guid LoanApplicationId, Dictionary<string, object> UpdatedProperties);