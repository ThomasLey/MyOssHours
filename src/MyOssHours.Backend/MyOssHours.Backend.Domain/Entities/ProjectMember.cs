using MyOssHours.Backend.Domain.Enumerations;
using MyOssHours.Backend.Domain.ValueObjects;

namespace MyOssHours.Backend.Domain.Entities;

public class ProjectMember
{
    private ProjectMember(UserId userId, PermissionLevel role)
    {
        UserId = userId;
        Role = role;
    }

    public UserId UserId { get; }
    public PermissionLevel Role { get; }

    // create method
    public static ProjectMember Create(UserId userId, PermissionLevel role)
    {
        return new ProjectMember(userId, role);
    }
}