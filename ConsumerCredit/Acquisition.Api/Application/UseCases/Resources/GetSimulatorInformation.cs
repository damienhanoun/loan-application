using Acquisition.Api.Infrastructure.Persistence.ReadRepositories;
using FastEndpoints;

namespace Acquisition.Api.Application.UseCases.Resources;

public class GetSimulatorInformation(IReadResourcesRepository resourcesRepository) : EndpointWithoutRequest<GetSimulatorInformationResponseDto>
{
    public override void Configure()
    {
        Post("/get-simulator-information");
        AllowAnonymous();
        Description(x => x
            .WithTags("Get simulator information")
            .Produces(200, typeof(GetSimulatorInformationResponseDto), "application/json")
            .ProducesProblem(500));
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var responseDto = await resourcesRepository.GetSimulatorInformation();
        await SendAsync(responseDto, cancellation: ct);
    }
}

public record GetSimulatorInformationResponseDto(List<string> Projects, List<string> Amounts, List<string> Maturities);