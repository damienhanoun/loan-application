using Acquisition.Application.LoanApplication;
using Acquisition.Infrastructure;

namespace Acquisition.Api.Scaffolding;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddTransient<ILoanApplicationRepository, LoanApplicationRepository>();

        return services;
    }
}