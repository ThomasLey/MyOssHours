using System.ComponentModel.DataAnnotations.Schema;

namespace MyOssHours.Backend.Infrastructure.Model;

[Table("ProjectHour")]
internal class ProjectHourEntity
{
    public long Id { get; set; }
    public Guid Uuid { get; set; }
    public DateTime StartDate { get; set; }
    public TimeSpan Duration { get; set; }
    public required UserEntity User { get; set; }
    public required WorkItemEntity WorkItem { get; set; }
    public required string Description { get; set; }
}