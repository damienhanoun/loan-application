using Acquisition.Api.Infrastructure.Persistence.ReadRepositories;
using Acquisition.Api.Infrastructure.Persistence.WriteRepositories;
using FastEndpoints;

namespace Acquisition.Api.Application.UseCases.Entities.LoanApplications;

public class ChooseALoanOffer(
    IReadLoanApplicationRepository readLoanApplicationRepository,
    IWriteLoanApplicationRepository writeLoanApplicationRepository) : Endpoint<ChooseALoanOfferCommand>
{
    public override void Configure()
    {
        Post("/choose-a-loan-offer");
        AllowAnonymous();
        Description(x => x
            .WithTags("Choose a loan offer")
            .Produces(200, typeof(void), "application/json")
            .ProducesProblem(500));
    }

    public override async Task HandleAsync(ChooseALoanOfferCommand request, CancellationToken ct)
    {
        var loanApplication = readLoanApplicationRepository.GetLoanApplication(request.LoanApplicationId);
        loanApplication.ChooseALoanOffer(request.OfferId);
        await writeLoanApplicationRepository.UpdateLoanApplication(loanApplication);
        await SendOkAsync(ct);
    }
}

public record ChooseALoanOfferCommand(Guid LoanApplicationId, Guid OfferId);