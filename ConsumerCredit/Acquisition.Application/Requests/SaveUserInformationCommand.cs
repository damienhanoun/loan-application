using Acquisition.Application.Repositories;
using Mediator;

namespace Acquisition.Application.Requests;

public record SaveUserInformationCommand(Guid LoanApplicationId, string Email) : ICommand;

public class SaveUserInformationHandler(ILoanApplicationRepository loanApplicationRepository) : ICommandHandler<SaveUserInformationCommand>
{
    public ValueTask<Unit> Handle(SaveUserInformationCommand command, CancellationToken cancellationToken)
    {
        var loanApplication = loanApplicationRepository.GetLoanApplication(command.LoanApplicationId);
        loanApplication.SaveUserInformation(command.Email);
        loanApplicationRepository.UpdateLoanApplication(loanApplication);
        return ValueTask.FromResult(Unit.Value);
    }
}