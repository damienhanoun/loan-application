using Acquisition.Api.Client;
using FastEndpoints;

namespace Acquisition.WebApplication.Server.Application.UseCases.LoanApplications;

public class ExpressLoanWish(IAcquisitionApiClient acquisitionClient)
    : Endpoint<ExpressLoanWishCommand, ExpressLoanWishResponseDto>
{
    public override void Configure()
    {
        Post("/express-loan-wish");
        AllowAnonymous();
        Description(x => x
            .WithTags("Loan application")
            .Produces(200, typeof(ExpressLoanWishResponseDto), "application/json")
            .ProducesProblem(500));
    }

    public override async Task HandleAsync(ExpressLoanWishCommand request, CancellationToken ct)
    {
        var responseDto = await acquisitionClient.ExpressLoanWishAsync(request, ct);
        await SendOkAsync(responseDto, ct);
    }
}