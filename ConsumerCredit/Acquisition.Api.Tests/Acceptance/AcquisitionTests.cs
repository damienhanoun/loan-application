using System.Net.Http.Json;
using Acquisition.Api.Tests.Helpers;
using Acquisition.Application.Dtos;
using Acquisition.Application.Requests;
using NFluent;

namespace Acquisition.Api.Tests.Acceptance;

public class AcquisitionTests(AcquisitionApiFactory waf) : IClassFixture<AcquisitionApiFactory>, IAsyncLifetime
{
    private readonly HttpClient _client = waf.Client;
    private readonly Func<Task> _dbReset = waf.ResetDatabaseAsync;

    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task DisposeAsync()
    {
        await _dbReset();
    }

    [Fact]
    public async Task Should_get_offer_according_to_prospect_wished_amount()
    {
        // Given
        var borrowedAmount = 1000;
        var request = new ExpressLoanWishCommand("Green investment", borrowedAmount, 12);
        var expressLoanWishResponse = await _client.PostAsJsonAsync("express-loan-wish", request);
        var expressLoanWishResponseDto = await expressLoanWishResponse.Content.ReadFromJsonAsync<ExpressLoanWishResponseDto>();
        var loanApplicationId = expressLoanWishResponseDto!.LoanApplicationId;

        // And
        var requestSaveUserInformation = new SaveUserInformationCommand(loanApplicationId, "email@email.fr");
        await _client.PostAsJsonAsync("save-user-information", requestSaveUserInformation);

        // When
        var getLoanOffersQuery = new GetLoanOffersQuery(loanApplicationId);
        var getLoanOffersResponse = await _client.PostAsJsonAsync("get-loan-offers", getLoanOffersQuery);
        var getLoanOffersResponseDto = await getLoanOffersResponse.Content.ReadFromJsonAsync<GetLoanOffersResponseDto>();
        var loanOffers = getLoanOffersResponseDto!.LoanOffers;

        // Assert
        Check.That(loanOffers.Select(l => l.Amount)).Contains(borrowedAmount);
    }
}