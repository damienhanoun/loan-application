using Acquisition.Application.Dtos;
using Acquisition.Application.Requests;
using FastEndpoints;
using Mediator;

namespace Acquisition.Api.EndPoints;

public class GetLoanOffers(IMediator mediator) : Endpoint<GetLoanOffersQuery, GetLoanOffersResponseDto>
{
    public override void Configure()
    {
        Post("/get-loan-offers");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetLoanOffersQuery query, CancellationToken ct)
    {
        var responseDto = await mediator.Send(query, ct);
        await SendOkAsync(responseDto, ct);
    }
}