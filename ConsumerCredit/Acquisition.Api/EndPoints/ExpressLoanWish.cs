using Acquisition.Application.Dtos;
using Acquisition.Application.Repositories;
using Acquisition.Domain.Entities;
using Acquisition.Domain.ValueObjects;
using FastEndpoints;
using Riok.Mapperly.Abstractions;

namespace Acquisition.Api.EndPoints;

public record ExpressLoanWishCommand(string Project, decimal Amount, int Maturity);

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
        var responseDto = new ExpressLoanWishResponseDto { LoanApplicationId = loanApplicationId };
        await SendOkAsync(responseDto, ct);
    }
}

[Mapper]
[UseStaticMapper(typeof(ValueObjectMappers))]
public static partial class InitialLoanWishMapper
{
    public static partial InitialLoanWish ToInitialLoanWish(this ExpressLoanWishCommand command);
}