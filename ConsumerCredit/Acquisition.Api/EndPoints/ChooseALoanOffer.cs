using Acquisition.Application.Requests;
using FastEndpoints;
using Mediator;

namespace Acquisition.Api.EndPoints;

public class ChooseALoanOffer(IMediator mediator) : Endpoint<ChooseALoanOfferCommand>
{
    public override void Configure()
    {
        Post("/choose-a-loan-offer");
        AllowAnonymous();
    }

    public override async Task HandleAsync(ChooseALoanOfferCommand command, CancellationToken ct)
    {
        await mediator.Send(command, ct);
        await SendOkAsync(ct);
    }
}