using System.ComponentModel.DataAnnotations.Schema;

namespace MyOssHours.Backend.Infrastructure.Model;

[Table("ProjectHour")]
internal class ProjectHourEntity
{
    public long Id { get; set; }
    public Guid Uuid { get; set; }
    public DateTime StartDate { get; set; }
    public TimeSpan Duration { get; set; }
    public UserEntity User { get; set; }
    public ProjectEntity Project { get; set; }
    public string Description { get; set; }
}