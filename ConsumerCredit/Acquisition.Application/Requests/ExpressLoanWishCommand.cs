using Acquisition.Application.Dtos;
using Acquisition.Application.Repositories;
using Acquisition.Domain.Entities;
using Acquisition.Domain.ValueObjects;
using Mediator;
using Riok.Mapperly.Abstractions;

namespace Acquisition.Application.Requests;

public record ExpressLoanWishCommand(string Project, decimal Amount, int Maturity) : ICommand<ExpressLoanWishResponseDto>;

public class ExpressLoanWishHandler(ILoanApplicationRepository loanApplicationRepository) : ICommandHandler<ExpressLoanWishCommand, ExpressLoanWishResponseDto>
{
    public async ValueTask<ExpressLoanWishResponseDto> Handle(ExpressLoanWishCommand command, CancellationToken cancellationToken)
    {
        // Should loan application creation be decoupled using domain event ?
        var loanApplicationId = Guid.NewGuid();
        var loanApplication = new LoanApplication(loanApplicationId);
        loanApplication.SetInitialLoanWish(command.ToInitialLoanWish());
        await loanApplicationRepository.CreateLoanApplication(loanApplication);
        return new ExpressLoanWishResponseDto { LoanApplicationId = loanApplicationId };
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

    public static partial InitialLoanWish ToInitialLoanWish(this ExpressLoanWishCommand command);
}