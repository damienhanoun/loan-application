using Acquisition.Api.Infrastructure.Persistence.ReadRepositories;
using Acquisition.Api.Infrastructure.Persistence.WriteRepositories;
using FastEndpoints;

namespace Acquisition.Api.Application.UseCases;

public class ChooseALoanOffer(
    IReadLoanApplicationRepository readLoanApplicationRepository,
    IWriteLoanApplicationRepository writeLoanApplicationRepository) : Endpoint<ChooseALoanOfferCommand>
{
    public override void Configure()
    {
        Post("/choose-a-loan-offer");
        AllowAnonymous();
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