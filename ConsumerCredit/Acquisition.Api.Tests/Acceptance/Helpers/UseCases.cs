using System.Net.Http.Json;
using Acquisition.Application.Dtos;
using Acquisition.Application.Requests;

namespace Acquisition.Api.Tests.Acceptance.Helpers;

public class UseCases
{
    private readonly HttpClient _client;

    public UseCases(HttpClient client)
    {
        _client = client;
    }

    public async Task<Guid> ExpressLoanWish(string project, int borrowedAmount, int maturityInMonths)
    {
        var request = new ExpressLoanWishCommand(project, borrowedAmount, maturityInMonths);
        var expressLoanWishResponse = await _client.PostAsJsonAsync("express-loan-wish", request);
        var expressLoanWishResponseDto = await expressLoanWishResponse.Content.ReadFromJsonAsync<ExpressLoanWishResponseDto>();
        return expressLoanWishResponseDto!.LoanApplicationId;
    }

    public Task SaveUserInformation(Guid loanApplicationId, string email)
    {
        var requestSaveUserInformation = new SaveUserInformationCommand(loanApplicationId, email);
        return _client.PostAsJsonAsync("save-user-information", requestSaveUserInformation);
    }

    public async Task<IEnumerable<LoanOfferDto>> GetLoanOffers(Guid loanApplicationId)
    {
        var getLoanOffersQuery = new GetLoanOffersQuery(loanApplicationId);
        var getLoanOffersResponse = await _client.PostAsJsonAsync("get-loan-offers", getLoanOffersQuery);
        var getLoanOffersResponseDto = await getLoanOffersResponse.Content.ReadFromJsonAsync<GetLoanOffersResponseDto>();
        return getLoanOffersResponseDto!.LoanOffers;
    }
}