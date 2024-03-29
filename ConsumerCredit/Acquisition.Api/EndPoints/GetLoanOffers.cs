using Acquisition.Api.Scaffolding;
using Acquisition.Application.Requests;
using Mediator;

namespace Acquisition.Api.EndPoints;

public class GetLoanOffers : IEndPoint
{
    public string Url => "get-loan-offers";

    public Delegate Handler => async (IMediator mediator, GetLoanOffersQuery query) =>
    {
        var responseDto = await mediator.Send(query);
        return Results.Ok(responseDto);
    };
}