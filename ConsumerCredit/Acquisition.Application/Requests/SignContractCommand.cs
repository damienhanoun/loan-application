using Acquisition.Application.Repositories;
using Mediator;

namespace Acquisition.Application.Requests;

public record SignContractCommand(Guid LoanApplicationId) : ICommand;

public class SignContractHandler(ILoanContractRepository loanContractRepository) : ICommandHandler<SignContractCommand>
{
    public async ValueTask<Unit> Handle(SignContractCommand request, CancellationToken cancellationToken)
    {
        var loanContract = loanContractRepository.GetLoanContract(request.LoanApplicationId);
        loanContract.SignContract();
        await loanContractRepository.UpdateLoanContract(loanContract);
        return Unit.Value;
    }
}