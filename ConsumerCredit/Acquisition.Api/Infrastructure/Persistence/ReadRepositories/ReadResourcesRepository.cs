using Acquisition.Api.Application.UseCases.Resources;
using Acquisition.Api.Infrastructure.Persistence.Database;
using AutomaticInterface;
using Microsoft.EntityFrameworkCore;

namespace Acquisition.Api.Infrastructure.Persistence.ReadRepositories;

[GenerateAutomaticInterface]
public class ReadResourcesRepository(AcquisitionContext context) : IReadResourcesRepository
{
    public async Task<GetSimulatorInformationResponseDto> GetSimulatorInformation()
    {
        var projects = await context.Resources.Where(r => r.Type == "project").Select(p => p.Value).ToListAsync();
        var amounts = await context.Resources.Where(r => r.Type == "amount").Select(a => a.Value).ToListAsync();
        var maturities = await context.Resources.Where(r => r.Type == "maturity").Select(m => m.Value).ToListAsync();

        return new GetSimulatorInformationResponseDto(projects, amounts, maturities);
    }
}