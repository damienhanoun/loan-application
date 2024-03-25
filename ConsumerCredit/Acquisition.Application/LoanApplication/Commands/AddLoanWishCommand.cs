using Acquisition.Domain.ValueObjects;
using Mediator;
using Riok.Mapperly.Abstractions;

namespace Acquisition.Application.LoanApplication.Commands;

public record AddLoanWishCommand(Guid LoanApplicationId, string Project, decimal Amount, int Maturity) : ICommand;

public class AddLoanWishHandler : ICommandHandler<AddLoanWishCommand>
{
    private readonly ILoanApplicationRepository loanApplicationRepository;

    public AddLoanWishHandler(ILoanApplicationRepository loanApplicationRepository)
    {
        this.loanApplicationRepository = loanApplicationRepository;
    }

    public ValueTask<Unit> Handle(AddLoanWishCommand command, CancellationToken cancellationToken)
    {
        var loanApplication = loanApplicationRepository.GetLoanApplication(command.LoanApplicationId);
        var initialLoanWish = command.ToInitialLoanWish();
        loanApplication.SetInitialLoanWish(initialLoanWish);
        loanApplicationRepository.UpdateLoanApplication(loanApplication);
        return Unit.ValueTask;
    }
}

[Mapper]
public static partial class InitialLoanWishMapper
{
    [ObjectFactory]
    private static Project CreateProject(string project)
    {
        return Project.Create(project);
    }

    [ObjectFactory]
    private static Amount CreateAmount(decimal amount)
    {
        return Amount.Create(amount);
    }

    [ObjectFactory]
    private static Maturity CreateMaturity(int maturity)
    {
        return Maturity.Create(maturity);
    }

    public static partial InitialLoanWish ToInitialLoanWish(this AddLoanWishCommand command);
}