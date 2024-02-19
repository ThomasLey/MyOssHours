using System.ComponentModel.DataAnnotations.Schema;

namespace MyOssHours.Backend.Infrastructure.Model;

[Table("User")]
internal class UserEntity
{
    public long Id { get; set; }
    public Guid Uuid { get; set; }
    public string Sid { get; set; }
    public string Nickname { get; set; }
    public string Email { get; set; }
}