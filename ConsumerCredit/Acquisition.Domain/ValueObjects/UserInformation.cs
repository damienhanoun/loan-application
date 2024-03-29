namespace Acquisition.Domain.ValueObjects;

public class UserInformation
{
    // Required by EF Core
    private UserInformation()
    {
    }

    private UserInformation(string email)
    {
        Email = Email.Create(email);
    }

    public Email Email { get; }

    public static UserInformation Create(string email)
    {
        return new UserInformation(email);
    }
}