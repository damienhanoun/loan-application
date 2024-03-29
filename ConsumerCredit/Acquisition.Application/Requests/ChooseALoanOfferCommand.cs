using Acquisition.Application.Repositories;
using Mediator;

namespace Acquisition.Application.Requests;

public record ChooseALoanOfferCommand(Guid LoanApplicationId, Guid OfferId) : ICommand;

public class ChooseALoanOfferHandler(ILoanApplicationRepository loanApplicationRepository) : ICommandHandler<ChooseALoanOfferCommand>
{
    public async ValueTask<Unit> Handle(ChooseALoanOfferCommand request, CancellationToken cancellationToken)
    {
        var loanApplication = loanApplicationRepository.GetLoanApplication(request.LoanApplicationId);
        loanApplication.ChooseALoanOffer(request.OfferId);
        await loanApplicationRepository.UpdateLoanApplication(loanApplication);
        return Unit.Value;
    }
}