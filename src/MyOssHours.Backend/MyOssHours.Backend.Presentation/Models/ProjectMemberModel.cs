using MyOssHours.Backend.Domain.Enumerations;

namespace MyOssHours.Backend.Presentation.Models;

public class ProjectMemberModel
{
    public PermissionLevel Role { get; set; }
    public Guid UserId { get; set; }
    public required string Nickname { get; set; }
}