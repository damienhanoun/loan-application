using Acquisition.Api.Client;
using FastEndpoints;

namespace LoanApplicationJourney.Bff.Application.UseCases.Resources;

public class GetSimulatorInformation(IAcquisitionApiClient acquisitionClient) : EndpointWithoutRequest<GetSimulatorInformationResponseDto>
{
    public override void Configure()
    {
        Post("/get-simulator-information");
        AllowAnonymous();
        Description(x => x
            .WithTags("Resources")
            .Produces(200, typeof(GetSimulatorInformationResponseDto), "application/json")
            .ProducesProblem(500));
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var responseDto = await acquisitionClient.GetSimulatorInformationAsync(ct);
        await SendAsync(responseDto, cancellation: ct);
    }
}