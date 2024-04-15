using Acquisition.Application.Dtos;
using Acquisition.Application.Repositories;
using Acquisition.Application.Services;
using Acquisition.Domain.Entities;
using Acquisition.Domain.ValueObjects;
using Mediator;
using Riok.Mapperly.Abstractions;

namespace Acquisition.Application.Requests;

public record GetLoanOffersQuery(Guid LoanApplicationId) : IRequest<GetLoanOffersResponseDto>;

public class GetLoanOffersHandler(
    ILoanApplicationRepository loanApplicationRepository,
    ILoanOffersService loanOffersService)
    : IRequestHandler<GetLoanOffersQuery, GetLoanOffersResponseDto>
{
    public ValueTask<GetLoanOffersResponseDto> Handle(GetLoanOffersQuery request, CancellationToken cancellationToken)
    {
        var loanApplication = loanApplicationRepository.GetLoanApplication(request.LoanApplicationId);
        var loanOffers = loanOffersService.GetLoanOffers(loanApplication.Id);
        return ValueTask.FromResult(new GetLoanOffersResponseDto { LoanOffers = loanOffers.ToDto() });
    }
}

[Mapper]
[UseStaticMapper(typeof(ValueObjectMappers))]
public static partial class OffersMapper
{
    public static partial List<LoanOfferDto> ToDto(this List<LoanOffer> loanOffers);
}