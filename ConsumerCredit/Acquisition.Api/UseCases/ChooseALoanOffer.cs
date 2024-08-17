using Acquisition.Api.Repositories;
using FastEndpoints;

namespace Acquisition.Api.UseCases;

public class ChooseALoanOffer(ILoanApplicationRepository loanApplicationRepository) : Endpoint<ChooseALoanOfferCommand>
{
    public override void Configure()
    {
        Post("/choose-a-loan-offer");
        AllowAnonymous();
    }

    public override async Task HandleAsync(ChooseALoanOfferCommand request, CancellationToken ct)
    {
        var loanApplication = loanApplicationRepository.GetLoanApplication(request.LoanApplicationId);
        loanApplication.ChooseALoanOffer(request.OfferId);
        await loanApplicationRepository.UpdateLoanApplication(loanApplication);
        await SendOkAsync(ct);
    }
}

public record ChooseALoanOfferCommand(Guid LoanApplicationId, Guid OfferId);