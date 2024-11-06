using Acquisition.Api.Domain.Entities;
using Acquisition.Api.Domain.ValueObjects;
using Acquisition.Api.Infrastructure.Persistence.WriteRepositories;
using FastEndpoints;
using Riok.Mapperly.Abstractions;

namespace Acquisition.Api.Application.UseCases.Entities.LoanApplications;

public class ExpressLoanWish(IWriteLoanApplicationRepository writeLoanApplicationRepository)
    : Endpoint<ExpressLoanWishCommand, ExpressLoanWishResponseDto>
{
    public override void Configure()
    {
        Post("/express-loan-wish");
        AllowAnonymous();
        Description(x => x
            .WithTags("Loan application")
            .Produces(200, typeof(ExpressLoanWishResponseDto), "application/json")
            .ProducesProblem(500));
    }

    public override async Task HandleAsync(ExpressLoanWishCommand request, CancellationToken ct)
    {
        var loanApplicationId = Guid.NewGuid();
        var loanApplication = new LoanApplication(loanApplicationId);
        loanApplication.SetInitialLoanWish(request.ToInitialLoanWish());
        await writeLoanApplicationRepository.CreateLoanApplication(loanApplication);
        var responseDto = new ExpressLoanWishResponseDto(loanApplicationId);
        await SendOkAsync(responseDto, ct);
    }
}

public record ExpressLoanWishCommand(string Project, decimal Amount, int Maturity);

public record ExpressLoanWishResponseDto(Guid LoanApplicationId);

[Mapper]
[UseStaticMapper(typeof(ValueObjectMappers))]
public static partial class InitialLoanWishMapper
{
    public static partial InitialLoanWish ToInitialLoanWish(this ExpressLoanWishCommand command);
}