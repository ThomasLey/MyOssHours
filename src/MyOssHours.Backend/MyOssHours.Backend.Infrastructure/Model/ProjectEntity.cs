using System.ComponentModel.DataAnnotations.Schema;

namespace MyOssHours.Backend.Infrastructure.Model;

[Table("Project")]
internal class ProjectEntity
{
    public long Id { get; set; }
    public Guid Uuid { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public virtual List<ProjectHourEntity> Hours { get; set; } = new();
    public virtual List<ProjectMemberEntity> Members { get; set; } = new();
}