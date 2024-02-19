using MyOssHours.Backend.Domain.ValueObjects;

namespace MyOssHours.Backend.Domain.Entities;

/// <summary>
///     This is the domain model for a user
/// </summary>
public class User
{
    private User(UserId uuid, string name, string email)
    {
        Uuid = uuid;
        Name = name;
        Email = email;
    }

    public UserId Uuid { get; }
    public string Name { get; }
    public string Email { get; }

    public static User Create(UserId id, string name, string email)
    {
        return new User(id, name, email);
    }
}