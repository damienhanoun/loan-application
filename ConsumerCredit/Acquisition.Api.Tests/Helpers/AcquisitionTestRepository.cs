using Acquisition.Domain.Entities;
using Acquisition.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using NpgsqlTypes;

namespace Acquisition.Api.Tests.Helpers;

public class AcquisitionTestRepository(AcquisitionApiFactory acquisitionApiFactory)
{
    public async Task<Guid> CreateALoanApplication(decimal wishedAmount = 5000, string email = "email@email.fr")
    {
        var loanApplicationId = Guid.NewGuid();
        await ExecuteSqlCommand(
            "INSERT INTO public.\"LoanApplications\" (\"Id\", \"InitialLoanWish_Amount\", \"UserInformation_Email\") VALUES (@id, @wishedAmount, @email)",
            [
                new NpgsqlParameter("id", loanApplicationId) { NpgsqlDbType = NpgsqlDbType.Uuid },
                new NpgsqlParameter("wishedAmount", wishedAmount) { NpgsqlDbType = NpgsqlDbType.Numeric },
                new NpgsqlParameter("email", email) { NpgsqlDbType = NpgsqlDbType.Varchar }
            ]);
        return loanApplicationId;
    }

    public async Task UpdateALoanApplication(Guid loanApplicationId, Guid loanOfferId)
    {
        await ExecuteSqlCommand(
            "UPDATE public.\"LoanApplications\" SET \"LoanOfferId\" = @loanOfferId  WHERE \"Id\" = @loanApplicationId",
            [
                new NpgsqlParameter("loanOfferId", loanOfferId) { NpgsqlDbType = NpgsqlDbType.Uuid },
                new NpgsqlParameter("loanApplicationId", loanApplicationId) { NpgsqlDbType = NpgsqlDbType.Uuid }
            ]);
    }

    public async Task<Guid> CreateALoanOffer()
    {
        var loanOfferId = Guid.NewGuid();
        await ExecuteSqlCommand(
            "INSERT INTO public.\"LoanOffers\" (\"Id\", \"Amount\", \"Maturity\", \"MonthlyAmount\") VALUES (@id, @amount, @maturity, @monthlyAmount)",
            [
                new NpgsqlParameter("id", loanOfferId) { NpgsqlDbType = NpgsqlDbType.Uuid },
                new NpgsqlParameter("amount", 5000) { NpgsqlDbType = NpgsqlDbType.Numeric },
                new NpgsqlParameter("maturity", 10) { NpgsqlDbType = NpgsqlDbType.Integer },
                new NpgsqlParameter("monthlyAmount", 500) { NpgsqlDbType = NpgsqlDbType.Numeric }
            ]);
        return loanOfferId;
    }

    public async Task<Guid> CreateALoanContract(Guid loanApplicationId, bool isSigned = false)
    {
        var loanContractId = Guid.NewGuid();
        await ExecuteSqlCommand(
            "INSERT INTO public.\"LoanContracts\" (\"Id\", \"LoanApplicationId\", \"IsSigned\") VALUES (@id, @loanApplicationId, @isSigned)",
            [
                new NpgsqlParameter("id", loanContractId) { NpgsqlDbType = NpgsqlDbType.Uuid },
                new NpgsqlParameter("loanApplicationId", loanApplicationId) { NpgsqlDbType = NpgsqlDbType.Uuid },
                new NpgsqlParameter("isSigned", isSigned) { NpgsqlDbType = NpgsqlDbType.Boolean }
            ]);
        return loanContractId;
    }

    public LoanApplication GetLoanApplication(Guid loanApplicationId)
    {
        using var scope = acquisitionApiFactory.Services.CreateScope();
        var acquisitionContext = scope.ServiceProvider.GetService<AcquisitionContext>()!;
        var loanApplication = acquisitionContext.LoanApplications
            .First(b => b.Id == loanApplicationId);
        return loanApplication;
    }

    private async Task ExecuteSqlCommand(string commandText, List<NpgsqlParameter> parameters)
    {
        await using var cmd = new NpgsqlCommand();
        cmd.Connection = acquisitionApiFactory.DbConnection;
        cmd.CommandText = commandText;
        parameters.ForEach(p => cmd.Parameters.Add(p));
        await cmd.ExecuteNonQueryAsync();
    }
}