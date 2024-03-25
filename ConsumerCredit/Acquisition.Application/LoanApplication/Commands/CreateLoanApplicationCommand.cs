using Acquisition.Application.LoanApplication.Dtos;
using Mediator;

namespace Acquisition.Application.LoanApplication.Commands;

public record CreateLoanApplicationCommand : ICommand<CreateLoanApplicationResponseDto>;

public class CreateLoanApplicationHandler : ICommandHandler<CreateLoanApplicationCommand, CreateLoanApplicationResponseDto>
{
    private readonly ILoanApplicationRepository _loanApplicationRepository;

    public CreateLoanApplicationHandler(ILoanApplicationRepository loanApplicationRepository)
    {
        _loanApplicationRepository = loanApplicationRepository;
    }

    public ValueTask<CreateLoanApplicationResponseDto> Handle(CreateLoanApplicationCommand command, CancellationToken cancellationToken)
    {
        var loanApplicationId = Guid.NewGuid();
        var loanApplication = new Domain.Entities.LoanApplication(loanApplicationId);
        _loanApplicationRepository.CreateLoanApplication(loanApplication);
        return ValueTask.FromResult(new CreateLoanApplicationResponseDto { LoanApplicationId = loanApplicationId });
    }
}