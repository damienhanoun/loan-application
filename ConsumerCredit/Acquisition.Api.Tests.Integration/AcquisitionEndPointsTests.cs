using System.Net;
using System.Net.Http.Json;
using Acquisition.Application.Dtos;
using Acquisition.Application.Requests;
using NFluent;

namespace Acquisition.Api.Tests.Integration;

public class AcquisitionEndPointsTests(AcquisitionApiFactory waf) : IClassFixture<AcquisitionApiFactory>, IAsyncLifetime
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
    public async Task Should_express_loan_wish()
    {
        // Arrange
        var request = new ExpressLoanWishCommand("Green investment", 10000, 12);

        // Act
        var response = await _client.PostAsJsonAsync("express-loan-wish", request);

        // Assert
        Check.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Should_save_user_email()
    {
        // Arrange
        var expressLoanWishResponseDto = await ExpressLoanWish();
        var request = new SaveUserInformationCommand(expressLoanWishResponseDto!.LoanApplicationId, "email@email.fr");

        // Act
        var response = await _client.PostAsJsonAsync("save-user-information", request);

        // Assert
        Check.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Should_evaluate_loan_eligibility_and_be_eligible()
    {
        // Arrange
        var expressLoanWishResponseDto = await ExpressLoanWish();

        var request = new EvaluateEligibilityToALoanQuery(expressLoanWishResponseDto!.LoanApplicationId);

        // Act
        var response = await _client.PostAsJsonAsync("evaluate-eligibility-to-a-loan", request);
        var responseDto = await response.Content.ReadFromJsonAsync<EvaluateEligibilityToALoanResponseDto>();

        // Assert
        Check.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);
        Check.That(responseDto!.IsEligibleToALoan).IsTrue();
    }

    [Fact]
    public async Task Should_get_loan_offers()
    {
        // Arrange
        var expressLoanWishResponseDto = await ExpressLoanWish();
        await EvaluateEligibilityToALoan(expressLoanWishResponseDto);

        var query = new GetLoanOffersQuery(expressLoanWishResponseDto!.LoanApplicationId);

        // Act
        var response = await _client.PostAsJsonAsync("get-loan-offers", query);
        var responseDto = await response.Content.ReadFromJsonAsync<GetLoanOffersResponseDto>();

        // Assert
        Check.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);
        Check.That(responseDto!.LoanOffers).Not.IsEmpty();
    }

    [Fact]
    public async Task Should_choose_a_loan_offer()
    {
        // Arrange
        var expressLoanWishResponseDto = await ExpressLoanWish();
        await SaveUserInformation(expressLoanWishResponseDto);
        await EvaluateEligibilityToALoan(expressLoanWishResponseDto);
        var getLoanOffersResponseDto = await GetLoanOffers(expressLoanWishResponseDto);

        var offer = getLoanOffersResponseDto!.LoanOffers.First();

        // Act
        var command = new ChooseALoanOfferCommand(expressLoanWishResponseDto!.LoanApplicationId, offer.Id);
        var response = await _client.PostAsJsonAsync("choose-a-loan-offer", command);

        // Assert
        Check.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Should_sign_the_contract()
    {
        // Arrange
        var expressLoanWishResponseDto = await ExpressLoanWish();
        await SaveUserInformation(expressLoanWishResponseDto);
        await EvaluateEligibilityToALoan(expressLoanWishResponseDto);
        var getLoanOffersResponseDto = await GetLoanOffers(expressLoanWishResponseDto);
        await ChooseAnOffer(getLoanOffersResponseDto, expressLoanWishResponseDto);

        // Act
        var command = new SignContractCommand(expressLoanWishResponseDto!.LoanApplicationId);
        var response = await _client.PostAsJsonAsync("sign-contract", command);

        // Assert
        Check.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);
    }

    private async Task ChooseAnOffer(GetLoanOffersResponseDto? getLoanOffersResponseDto, ExpressLoanWishResponseDto? expressLoanWishResponseDto)
    {
        var offer = getLoanOffersResponseDto!.LoanOffers.First();
        var command = new ChooseALoanOfferCommand(expressLoanWishResponseDto!.LoanApplicationId, offer.Id);
        await _client.PostAsJsonAsync("choose-a-loan-offer", command);
    }

    private async Task SaveUserInformation(ExpressLoanWishResponseDto? expressLoanWishResponseDto)
    {
        var requestSaveUserInformation = new SaveUserInformationCommand(expressLoanWishResponseDto!.LoanApplicationId, "email@email.fr");
        await _client.PostAsJsonAsync("save-user-information", requestSaveUserInformation);
    }

    private async Task<GetLoanOffersResponseDto?> GetLoanOffers(ExpressLoanWishResponseDto? expressLoanWishResponseDto)
    {
        var getLoanOffersQuery = new GetLoanOffersQuery(expressLoanWishResponseDto!.LoanApplicationId);
        var getLoanOffersResponse = await _client.PostAsJsonAsync("get-loan-offers", getLoanOffersQuery);
        var getLoanOffersResponseDto = await getLoanOffersResponse.Content.ReadFromJsonAsync<GetLoanOffersResponseDto>();
        return getLoanOffersResponseDto;
    }

    private async Task<ExpressLoanWishResponseDto?> ExpressLoanWish()
    {
        var expressLoanWishRequest = new ExpressLoanWishCommand("Green investment", 10000, 12);
        var expressLoanWishResponse = await _client.PostAsJsonAsync("express-loan-wish", expressLoanWishRequest);
        var expressLoanWishResponseDto = await expressLoanWishResponse.Content.ReadFromJsonAsync<ExpressLoanWishResponseDto>();
        return expressLoanWishResponseDto;
    }

    private async Task EvaluateEligibilityToALoan(ExpressLoanWishResponseDto? expressLoanWishResponseDto)
    {
        var evaluateEligibilityToALoanRequest = new EvaluateEligibilityToALoanQuery(expressLoanWishResponseDto!.LoanApplicationId);
        await _client.PostAsJsonAsync("evaluate-eligibility-to-a-loan", evaluateEligibilityToALoanRequest);
    }
}