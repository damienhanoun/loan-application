using Acquisition.Api.Repositories;
using FastEndpoints;

namespace Acquisition.Api.UseCases;

public class SignContract(ILoanContractRepository loanContractRepository)
    : Endpoint<SignContractCommand>
{
    public override void Configure()
    {
        Post("/sign-contract");
        AllowAnonymous();
    }

    public override async Task HandleAsync(SignContractCommand request, CancellationToken ct)
    {
        var loanContract = loanContractRepository.GetLoanContract(request.LoanApplicationId);
        loanContract.SignContract();
        await loanContractRepository.UpdateLoanContract(loanContract);
        await SendOkAsync(ct);
    }
}

public record SignContractCommand(Guid LoanApplicationId);