using Acquisition.Api.Domain.ValueObjects;

namespace Acquisition.Domain.ValueObjects;

public class Amount : ValueObject<decimal>
{
    // Required by EF Core
    private Amount()
    {
    }

    private Amount(decimal value)
    {
        Value = value;
    }

    public static Amount Create(decimal value)
    {
        if (value < 0) throw new InvalidOperationException("Amount cannot be negative");

        return new Amount(value);
    }

    public static Amount CreateWithoutValidation(decimal value)
    {
        return new Amount(value);
    }
}