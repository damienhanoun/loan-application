using Acquisition.Application.Dtos;
using Acquisition.Application.Requests;
using FastEndpoints;
using Mediator;

namespace Acquisition.Api.EndPoints;

public class ExpressLoanWish(IMediator mediator) : Endpoint<ExpressLoanWishCommand, ExpressLoanWishResponseDto>
{
    public override void Configure()
    {
        Post("/express-loan-wish");
        AllowAnonymous();
    }

    public override async Task HandleAsync(ExpressLoanWishCommand command, CancellationToken ct)
    {
        var responseDto = await mediator.Send(command, ct);
        await SendOkAsync(responseDto, ct);
    }
}