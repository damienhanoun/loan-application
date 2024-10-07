using Acquisition.Api.Domain.Entities;
using Acquisition.Api.Domain.ValueObjects;
using Acquisition.Api.Infrastructure.Persistence.ReadRepositories;
using FastEndpoints;
using Riok.Mapperly.Abstractions;

namespace Acquisition.Api.Application.UseCases.Entities.LoanOffers;

public class GetLoanOffers(
    IReadLoanApplicationRepository readLoanApplicationRepository,
    IReadLoanOffersRepository readLoanOffersRepository)
    : Endpoint<GetLoanOffersQuery, GetLoanOffersResponseDto>
{
    public override void Configure()
    {
        Post("/get-loan-offers");
        AllowAnonymous();
        Description(x => x
            .WithTags("Get loan offers")
            .Produces(200, typeof(GetLoanOffersResponseDto), "application/json")
            .ProducesProblem(500));
    }

    public override async Task HandleAsync(GetLoanOffersQuery request, CancellationToken ct)
    {
        var loanApplication = readLoanApplicationRepository.GetLoanApplication(request.LoanApplicationId);
        var loanOffers = readLoanOffersRepository.GetLoanOffers(loanApplication.InitialLoanWish!.Amount.Value);
        var responseDto = new GetLoanOffersResponseDto(loanOffers);
        await SendOkAsync(responseDto, ct);
    }
}

public record GetLoanOffersQuery(Guid LoanApplicationId);

public record GetLoanOffersResponseDto(IEnumerable<LoanOfferDto> LoanOffers);

public record LoanOfferDto
{
    public Guid Id { get; init; }
    public decimal Amount { get; init; }
    public int Maturity { get; init; }
    public decimal MonthlyAmount { get; init; }
}

[Mapper]
[UseStaticMapper(typeof(ValueObjectMappers))]
public static partial class OffersMapper
{
    public static partial List<LoanOfferDto> ToDto(this List<LoanOffer> loanOffers);
}