using System.Net.Http.Json;
using Acquisition.Api.Tests.Acceptance.Drivers;
using Acquisition.Application.Dtos;
using Acquisition.Application.Requests;
using NFluent;

namespace Acquisition.Api.Tests.Acceptance.Steps;

[Binding]
public class LoanOffersPropositionDefinition
{
    public readonly HttpClient _httpClient;
    private Guid _loanApplicationId;
    private IEnumerable<LoanOfferDto> _loanOffers;

    public LoanOffersPropositionDefinition()
    {
        var factory = new AcquisitionApiFactory();
        factory.InitializeAsync().Wait();
        _httpClient = factory.Client;
    }

    [Given(@"a user that wants to borrow (.*) euros")]
    public async Task GivenAUserThatWantsToBorrowEuros(int borrowedAmount)
    {
        var request = new ExpressLoanWishCommand("Green investment", 10000, 12);
        var expressLoanWishResponse = await _httpClient.PostAsJsonAsync("express-loan-wish", request);
        var expressLoanWishResponseDto = await expressLoanWishResponse.Content.ReadFromJsonAsync<ExpressLoanWishResponseDto>();
        _loanApplicationId = expressLoanWishResponseDto!.LoanApplicationId;
    }

    [Given(@"the user set his email")]
    public async Task GivenTheUserSetHisEmail()
    {
        var requestSaveUserInformation = new SaveUserInformationCommand(_loanApplicationId, "email@email.fr");
        await _httpClient.PostAsJsonAsync("save-user-information", requestSaveUserInformation);
    }

    [When(@"the user requests loan offers")]
    public async Task WhenTheUserRequestsLoanOffers()
    {
        var getLoanOffersQuery = new GetLoanOffersQuery(_loanApplicationId);
        var getLoanOffersResponse = await _httpClient.PostAsJsonAsync("get-loan-offers", getLoanOffersQuery);
        var getLoanOffersResponseDto = await getLoanOffersResponse.Content.ReadFromJsonAsync<GetLoanOffersResponseDto>();
        _loanOffers = getLoanOffersResponseDto!.LoanOffers;
    }

    [Then(@"the user should be proposed a loan offer of (.*?) euros")]
    public void ThenTheUserShouldBeProposedALoanOfferOf(int borrowedAmount)
    {
        Check.That(_loanOffers.Select(l => l.Amount)).Contains(borrowedAmount);
    }
}