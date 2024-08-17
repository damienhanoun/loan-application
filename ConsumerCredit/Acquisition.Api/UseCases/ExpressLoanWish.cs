using Acquisition.Api.Domain.Entities;
using Acquisition.Api.Domain.ValueObjects;
using Acquisition.Api.Repositories;
using FastEndpoints;
using Riok.Mapperly.Abstractions;

namespace Acquisition.Api.UseCases;

public class ExpressLoanWish(ILoanApplicationRepository loanApplicationRepository) : Endpoint<ExpressLoanWishCommand, ExpressLoanWishResponseDto>
{
    public override void Configure()
    {
        Post("/express-loan-wish");
        AllowAnonymous();
    }

    public override async Task HandleAsync(ExpressLoanWishCommand request, CancellationToken ct)
    {
        var loanApplicationId = Guid.NewGuid();
        var loanApplication = new LoanApplication(loanApplicationId);
        loanApplication.SetInitialLoanWish(request.ToInitialLoanWish());
        await loanApplicationRepository.CreateLoanApplication(loanApplication);
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