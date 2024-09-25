using FastEndpoints;

namespace Acquisition.Api.Application.UseCases.Resources;

public class GetSimulatorInformation : EndpointWithoutRequest<GetSimulatorInformationResponseDto>
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
        var responseDto = new GetSimulatorInformationResponseDto(
            GetProjects(),
            GetAmounts(),
            GetMaturities()
        );
        await SendAsync(responseDto, cancellation: ct);
    }

    private static List<int> GetMaturities()
    {
        return [6, 12, 24, 36, 48, 72, 84];
    }

    private static List<string> GetAmounts()
    {
        return
        [
            "1000",
            "1500",
            "2000",
            "2500",
            "3000",
            "3500",
            "4000",
            "4500",
            "5000",
            "5500",
            "6000",
            "6500",
            "7000",
            "7500",
            "8000",
            "8500",
            "9000",
            "9500",
            "+10000"
        ];
    }

    private static List<string> GetProjects()
    {
        return
        [
            "Wedding",
            "Home Renovation",
            "Vacation",
            "Debt Consolidation",
            "Car Purchase"
        ];
    }
}

public record GetSimulatorInformationResponseDto(List<string> Projects, List<string> Amounts, List<int> Maturities);