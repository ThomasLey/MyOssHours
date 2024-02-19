using System.ComponentModel.DataAnnotations.Schema;
using MyOssHours.Backend.Domain.Enumerations;

namespace MyOssHours.Backend.Infrastructure.Model;

[Table("ProjectMember")]
internal class ProjectMemberEntity
{
    public long Id { get; set; }
    public PermissionLevel Role { get; set; }
    public virtual UserEntity User { get; set; }
}