using Acquisition.Api.Client;
using FastEndpoints;

namespace Acquisition.WebApplication.Server.Application.UseCases.LoanApplications;

public class UpdateUserInformation(IAcquisitionApiClient acquisitionClient)
    : Endpoint<UpdateUserInformationCommand>
{
    public override void Configure()
    {
        Post("/update-user-information");
        AllowAnonymous();
        Description(x => x
            .WithTags("Loan application")
            .Produces(200, typeof(void), "application/json")
            .ProducesProblem(500));
    }

    public override async Task HandleAsync(UpdateUserInformationCommand request, CancellationToken ct)
    {
        await acquisitionClient.UpdateUserInformationAsync(request, ct);
        await SendOkAsync(ct);
    }
}