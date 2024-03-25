namespace Acquisition.Domain.ValueObjects;

public class Maturity : ValueObject<int>
{
    // Required by EF Core
    private Maturity()
    {
    }

    private Maturity(int value)
    {
        Value = value;
    }

    public static Maturity Create(int value)
    {
        return new Maturity(value);
    }

    public static Maturity CreateWithoutValidation(int value)
    {
        return new Maturity(value);
    }
}