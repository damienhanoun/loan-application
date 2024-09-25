using Acquisition.Api.Client;
using Acquisition.Api.Tests.Helpers;
using NFluent;

namespace Acquisition.Api.Tests.Integration;

public class AcquisitionEndPointsTests(AcquisitionApiFactory acquisitionApiFactory)
    : IClassFixture<AcquisitionApiFactory>, IAsyncLifetime
{
    private readonly AcquisitionTestRepository _acquisitionTestRepository = new(acquisitionApiFactory);
    private readonly AcquisitionApiClient _client = new(acquisitionApiFactory.Client.BaseAddress!.ToString(), acquisitionApiFactory.Client);
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
        var request = new ExpressLoanWishCommand
        {
            Project = "Green investment",
            Amount = 10000,
            Maturity = 12
        };

        // Act
        var exception = await Record.ExceptionAsync(() => _client.ExpressLoanWishAsync(request));

        // Assert
        Check.That(exception).IsNull();
    }

    [Fact]
    public async Task Should_save_user_email()
    {
        // Arrange
        var loanApplicationId = await _acquisitionTestRepository.CreateALoanApplication();
        var request = new UpdateUserInformationCommand
        {
            LoanApplicationId = loanApplicationId,
            UpdatedProperties = new Dictionary<string, object>
            {
                { "Email", "email@email.fr" }
            }
        };

        // Act
        var exception = await Record.ExceptionAsync(() => _client.UpdateUserInformationAsync(request));

        // Assert
        Check.That(exception).IsNull();
    }

    [Fact]
    public async Task Should_evaluate_loan_eligibility_and_be_eligible()
    {
        // Arrange
        var loanApplicationId = await _acquisitionTestRepository.CreateALoanApplication();
        var request = new EvaluateEligibilityToALoanQuery { LoanApplicationId = loanApplicationId };

        // Act
        var responseDto = await _client.EvaluateLoanEligibilityAsync(request);

        // Assert
        Check.That(responseDto!.IsEligibleToALoan).IsTrue();
    }

    [Fact]
    public async Task Should_get_loan_offers()
    {
        // Arrange
        var wishedAmount = 1000;
        var loanApplicationId = await _acquisitionTestRepository.CreateALoanApplication(wishedAmount);
        var query = new GetLoanOffersQuery { LoanApplicationId = loanApplicationId };

        // Act
        var responseDto = await _client.GetLoanOffersAsync(query);

        // Assert
        Check.That(responseDto!.LoanOffers).Not.IsEmpty();
    }

    [Fact]
    public async Task Should_choose_a_loan_offer()
    {
        // Arrange
        var loanApplicationId = await _acquisitionTestRepository.CreateALoanApplication();
        var loanOfferId = await _acquisitionTestRepository.CreateALoanOffer();
        var command = new ChooseALoanOfferCommand { LoanApplicationId = loanApplicationId, OfferId = loanOfferId };

        // Act
        await _client.ChooseALoanOfferAsync(command);

        // Assert
        var loanApplication = _acquisitionTestRepository.GetLoanApplication(loanApplicationId);
        Check.That(loanApplication.LoanOfferId).IsEqualTo(loanOfferId);
    }

    [Fact]
    public async Task Should_sign_the_contract()
    {
        // Arrange
        var loanApplicationId = Guid.NewGuid();
        await _acquisitionTestRepository.CreateALoanContract(loanApplicationId);
        var command = new SignContractCommand { LoanApplicationId = loanApplicationId };

        // Act
        var exception = await Record.ExceptionAsync(() => _client.SignContractAsync(command));

        // Assert
        Check.That(exception).IsNull();
    }

    [Fact]
    public async Task Should_get_simulator_information()
    {
        // Act
        var responseDto = await _client.GetSimulatorInformationAsync();

        // Assert
        Check.That(responseDto!.Projects).Not.IsEmpty();
        Check.That(responseDto.Amounts).Not.IsEmpty();
        Check.That(responseDto.Maturities).Not.IsEmpty();
    }
}