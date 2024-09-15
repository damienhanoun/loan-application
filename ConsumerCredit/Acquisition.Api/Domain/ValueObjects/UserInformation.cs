using Acquisition.Domain.ValueObjects;

namespace Acquisition.Api.Domain.ValueObjects;

public class UserInformation
{
    // Required by EF Core (constructor without parameter)

    public Email? Email { get; private set; }

    public void UpdateEmail(Email email)
    {
        Email = email;
    }
}