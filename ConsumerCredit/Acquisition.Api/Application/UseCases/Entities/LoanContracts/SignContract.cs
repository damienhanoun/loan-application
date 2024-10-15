using Acquisition.Api.Infrastructure.Persistence.ReadRepositories;
using Acquisition.Api.Infrastructure.Persistence.WriteRepositories;
using FastEndpoints;

namespace Acquisition.Api.Application.UseCases.Entities.LoanContracts;

public class SignContract(
    IReadLoanContractRepository readLoanContractRepository,
    IWriteLoanContractRepository writeLoanContractRepository)
    : Endpoint<SignContractCommand>
{
    public override void Configure()
    {
        Post("/sign-contract");
        AllowAnonymous();
        Description(x => x
            .WithTags("Loan contract")
            .Produces(200, typeof(void), "application/json")
            .ProducesProblem(500));
    }

    public override async Task HandleAsync(SignContractCommand request, CancellationToken ct)
    {
        var loanContract = readLoanContractRepository.GetLoanContract(request.LoanApplicationId);
        loanContract.SignContract();
        await writeLoanContractRepository.UpdateLoanContract(loanContract);
        await SendOkAsync(ct);
    }
}

public record SignContractCommand(Guid LoanApplicationId);