namespace Acquisition.Domain.ValueObjects;

public class Project : ValueObject<string>
{
    // Required by EF Core
    private Project()
    {
    }

    private Project(string value)
    {
        Value = value;
    }

    public static Project Create(string value)
    {
        return new Project(value);
    }

    public static Project CreateWithoutValidation(string value)
    {
        return new Project(value);
    }
}