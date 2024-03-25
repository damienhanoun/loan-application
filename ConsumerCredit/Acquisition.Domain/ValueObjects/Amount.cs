namespace Acquisition.Domain.ValueObjects;

public class Amount : ValueObject<decimal>
{
    // Required by EF Core
    private Amount()
    {
    }

    private Amount(decimal value)
    {
        if (Value < 0) throw new ArgumentException("Amount cannot be negative");

        Value = value;
    }

    public static Amount Create(decimal value)
    {
        return new Amount(value);
    }

    public static Amount CreateWithoutValidation(decimal value)
    {
        return new Amount(value);
    }
}