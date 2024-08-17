using System.Net;
using System.Net.Http.Json;
using Acquisition.Api.EndPoints;
using Acquisition.Api.Tests.Helpers;
using NFluent;

namespace Acquisition.Api.Tests.Integration;

public class AcquisitionEndPointsTests(AcquisitionApiFactory acquisitionApiFactory)
    : IClassFixture<AcquisitionApiFactory>, IAsyncLifetime
{
    private readonly AcquisitionTestRepository _acquisitionTestRepository = new(acquisitionApiFactory);
    private readonly HttpClient _client = acquisitionApiFactory.Client;
    private readonly Func<Task> _dbReset = acquisitionApiFactory.ResetDatabaseAsync;

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
        var loanApplicationId = await _acquisitionTestRepository.CreateALoanApplication();
        var request = new SaveUserInformationCommand(loanApplicationId, "email@email.fr");

        // Act
        var response = await _client.PostAsJsonAsync("save-user-information", request);

        // Assert
        Check.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Should_evaluate_loan_eligibility_and_be_eligible()
    {
        // Arrange
        var loanApplicationId = await _acquisitionTestRepository.CreateALoanApplication();
        var request = new EvaluateEligibilityToALoanQuery(loanApplicationId);

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
        var wishedAmount = 1000;
        var loanApplicationId = await _acquisitionTestRepository.CreateALoanApplication(wishedAmount);
        var query = new GetLoanOffersQuery(loanApplicationId);

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
        var loanApplicationId = await _acquisitionTestRepository.CreateALoanApplication();
        var loanOfferId = await _acquisitionTestRepository.CreateALoanOffer();

        // Act
        var command = new ChooseALoanOfferCommand(loanApplicationId, loanOfferId);
        var response = await _client.PostAsJsonAsync("choose-a-loan-offer", command);

        // Assert
        var loanApplication = _acquisitionTestRepository.GetLoanApplication(loanApplicationId);
        Check.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);
        Check.That(loanApplication.LoanOfferId).IsEqualTo(loanOfferId);
    }

    [Fact]
    public async Task Should_sign_the_contract()
    {
        // Arrange
        var loanApplicationId = Guid.NewGuid();
        await _acquisitionTestRepository.CreateALoanContract(loanApplicationId);

        // Act
        var command = new SignContractCommand(loanApplicationId);
        var response = await _client.PostAsJsonAsync("sign-contract", command);

        // Assert
        Check.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);
    }
}