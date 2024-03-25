using System.Net;
using System.Net.Http.Json;
using Acquisition.Application.LoanApplication.Commands;
using Acquisition.Application.LoanApplication.Dtos;
using Acquisition.Application.LoanApplication.Queries;
using NFluent;

namespace Acquisition.Api.Tests.Integration;

public class AcquisitionEndPointsTests : IClassFixture<AcquisitionApiFactory>, IAsyncLifetime
{
    private readonly HttpClient _client;
    private readonly Func<Task> _dbReset;

    public AcquisitionEndPointsTests(AcquisitionApiFactory waf)
    {
        _client = waf.Client;
        _dbReset = waf.ResetDatabaseAsync;
    }

    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task DisposeAsync()
    {
        await _dbReset();
    }

    [Fact]
    public async Task Should_create_a_loan_application()
    {
        // Arrange
        var request = new CreateLoanApplicationCommand();

        // Act
        var response = await _client.PostAsJsonAsync("loan-applications/create", request);
        var responseDto = await response.Content.ReadFromJsonAsync<CreateLoanApplicationResponseDto>();

        // Assert
        Check.That(response.StatusCode).IsEqualTo(HttpStatusCode.Created);
        Check.That(responseDto!.LoanApplicationId).Not.IsEqualTo(Guid.Empty);
    }

    [Fact]
    public async Task Should_add_the_loan_application_initial_wish()
    {
        // Arrange
        // TODO: Find a way to replace by putting data in the database - is it ok if data change in the future ?
        var createResponse = await _client.PostAsJsonAsync("loan-applications/create", new CreateLoanApplicationCommand());
        var createResponseDto = await createResponse.Content.ReadFromJsonAsync<CreateLoanApplicationResponseDto>();

        var request = new AddLoanWishCommand(createResponseDto!.LoanApplicationId, "Green investment", 10000, 12);

        // Act
        var response = await _client.PostAsJsonAsync("loan-applications/add-loan-wish", request);

        // Assert
        Check.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Should_evaluate_loan_eligibility_and_be_eligible()
    {
        // Arrange
        var createResponse = await _client.PostAsJsonAsync("loan-applications/create", new CreateLoanApplicationCommand());
        var createResponseDto = await createResponse.Content.ReadFromJsonAsync<CreateLoanApplicationResponseDto>();

        var request = new EvaluateEligibilityToALoanQuery(createResponseDto!.LoanApplicationId);

        // Act
        var response = await _client.PostAsJsonAsync("loan-applications/evaluate-eligibility-to-a-loan", request);
        var responseDto = await response.Content.ReadFromJsonAsync<EvaluateEligibilityToALoanResponseDto>();

        // Assert
        Check.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);
        Check.That(responseDto!.IsEligibileToALoan).IsTrue();
    }

    [Fact]
    public async Task Should_get_offers_eligible_to_the_loan_application()
    {
        // Arrange
        var createResponse = await _client.PostAsJsonAsync("loan-applications/create", new CreateLoanApplicationCommand());
        var createResponseDto = await createResponse.Content.ReadFromJsonAsync<CreateLoanApplicationResponseDto>();

        var request = new EvaluateEligibilityToALoanQuery(createResponseDto!.LoanApplicationId);

        // Act
        var response = await _client.PostAsJsonAsync("loan-applications/evaluate-eligibility-to-a-loan", request);
        var responseDto = await response.Content.ReadFromJsonAsync<EvaluateEligibilityToALoanResponseDto>();

        // Assert
        Check.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);
        Check.That(responseDto!.IsEligibileToALoan).IsTrue();
    }
}