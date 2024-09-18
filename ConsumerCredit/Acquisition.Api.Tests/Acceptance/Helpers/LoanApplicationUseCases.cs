using Acquisition.Api.Client;

namespace Acquisition.Api.Tests.Acceptance.Helpers;

public class LoanApplicationUseCases
{
    private readonly AcquisitionApiClient _client;

    public LoanApplicationUseCases(HttpClient client)
    {
        _client = new AcquisitionApiClient(client.BaseAddress!.ToString(), client);
    }

    public async Task<Guid> ExpressLoanWish(string project, int borrowedAmount, int maturityInMonths)
    {
        var request = new ExpressLoanWishCommand { Project = project, Amount = borrowedAmount, Maturity = maturityInMonths };
        var expressLoanWishResponseDto = await _client.ExpressLoanWishAsync(request);
        return expressLoanWishResponseDto!.LoanApplicationId;
    }

    public Task SaveUserInformation(Guid loanApplicationId, string email)
    {
        var userInformation = new Dictionary<string, object> { { "Email", email } };
        var requestSaveUserInformation = new UpdateUserInformationCommand { LoanApplicationId = loanApplicationId, UpdatedProperties = userInformation };
        return _client.UpdateUserInformationAsync(requestSaveUserInformation);
    }

    public async Task<IEnumerable<LoanOfferDto>> GetLoanOffers(Guid loanApplicationId)
    {
        var getLoanOffersQuery = new GetLoanOffersQuery { LoanApplicationId = loanApplicationId };
        var getLoanOffersResponseDto = await _client.GetLoanOffersAsync(getLoanOffersQuery);
        return getLoanOffersResponseDto!.LoanOffers;
    }
}