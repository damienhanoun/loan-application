using Acquisition.Api.Tests.Acceptance.Helpers;
using Acquisition.Api.Tests.Helpers;
using NFluent;

namespace Acquisition.Api.Tests.Acceptance;

public class AcquisitionTests(AcquisitionApiFactory waf) : IClassFixture<AcquisitionApiFactory>, IAsyncLifetime
{
    private readonly Func<Task> _dbReset = waf.ResetDatabaseAsync;
    private readonly UseCases _useCases = new(waf.Client);

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
        var loanApplicationId = await _useCases.ExpressLoanWish("Green investment", borrowedAmount, 12);

        // And
        await _useCases.SaveUserInformation(loanApplicationId, "email@email.fr");

        // When
        var loanOffers = await _useCases.GetLoanOffers(loanApplicationId);

        // Then
        Check.That(loanOffers.Select(l => l.Amount)).Contains(borrowedAmount);
    }
}