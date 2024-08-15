using Acquisition.Application.Dtos;
using Acquisition.Application.Repositories;
using Acquisition.Application.Services;
using Acquisition.Domain.Entities;
using Acquisition.Domain.ValueObjects;
using FastEndpoints;
using Riok.Mapperly.Abstractions;

namespace Acquisition.Api.EndPoints;

public record GetLoanOffersQuery(Guid LoanApplicationId);

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
        var responseDto = new GetLoanOffersResponseDto { LoanOffers = loanOffers.ToDto() };
        await SendOkAsync(responseDto, ct);
    }
}

[Mapper]
[UseStaticMapper(typeof(ValueObjectMappers))]
public static partial class OffersMapper
{
    public static partial List<LoanOfferDto> ToDto(this List<LoanOffer> loanOffers);
}