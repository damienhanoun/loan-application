using Acquisition.Application.Repositories;
using Acquisition.Application.Services;
using Acquisition.Infrastructure;

namespace Acquisition.Api.Scaffolding;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddTransient<ILoanApplicationRepository, LoanApplicationRepository>();
        services.AddTransient<ILoanOffersEligibilityEvaluationService, LoanOffersEligibilityEvaluationService>();
        services.AddTransient<ILoanOffersService, LoanOffersService>();
        services.AddTransient<ILoanContractRepository, LoanContractRepository>();
        services.AddTransient<ICommunicationService, CommunicationService>();

        return services;
    }
}