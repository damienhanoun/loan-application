using Acquisition.Api.Domain.ValueObjects;

namespace Acquisition.Domain.ValueObjects;

public class Email : ValueObject<string>
{
    // Required by EF Core
    private Email()
    {
    }

    private Email(string value)
    {
        Value = value;
    }

    public static Email Create(string value)
    {
        return new Email(value);
    }

    public static Email CreateWithoutValidation(string value)
    {
        return new Email(value);
    }
}