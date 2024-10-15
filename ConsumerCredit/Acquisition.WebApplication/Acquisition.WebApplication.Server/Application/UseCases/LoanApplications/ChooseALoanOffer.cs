using Acquisition.Api.Client;
using FastEndpoints;

namespace Acquisition.WebApplication.Server.Application.UseCases.LoanApplications;

public class ChooseALoanOffer(IAcquisitionApiClient acquisitionClient) : Endpoint<ChooseALoanOfferCommand>
{
    public override void Configure()
    {
        Post("/choose-a-loan-offer");
        AllowAnonymous();
        Description(x => x
            .WithTags("Loan application")
            .Produces(200, typeof(void), "application/json")
            .ProducesProblem(500));
    }

    public override async Task HandleAsync(ChooseALoanOfferCommand request, CancellationToken ct)
    {
        await acquisitionClient.ChooseALoanOfferAsync(request, ct);
        await SendOkAsync(ct);
    }
}