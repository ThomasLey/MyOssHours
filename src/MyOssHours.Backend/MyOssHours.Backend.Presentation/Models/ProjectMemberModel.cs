using MyOssHours.Backend.Domain.Enumerations;

namespace MyOssHours.Backend.Presentation.Models;

public class ProjectMemberModel
{
    public Guid Uuid { get; set; }
    public PermissionLevel Role { get; set; }
    public UserModel User { get; set; }
}