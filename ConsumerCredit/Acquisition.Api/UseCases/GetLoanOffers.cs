using Acquisition.Api.Domain.Entities;
using Acquisition.Api.Domain.ValueObjects;
using Acquisition.Api.Repositories;
using Acquisition.Api.Services;
using FastEndpoints;
using Riok.Mapperly.Abstractions;

namespace Acquisition.Api.UseCases;

public class GetLoanOffers(
    ILoanApplicationRepository loanApplicationRepository,
    ILoanOffersService loanOffersService) : Endpoint<GetLoanOffersQuery, GetLoanOffersResponseDto>
{
    public override void Configure()
    {
        Post("/get-loan-offers");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetLoanOffersQuery request, CancellationToken ct)
    {
        var loanApplication = loanApplicationRepository.GetLoanApplication(request.LoanApplicationId);
        var loanOffers = loanOffersService.GetLoanOffers(loanApplication.Id);
        var responseDto = new GetLoanOffersResponseDto(loanOffers.ToDto());
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