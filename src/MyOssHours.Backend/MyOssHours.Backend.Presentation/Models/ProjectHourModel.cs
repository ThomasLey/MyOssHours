namespace MyOssHours.Backend.Presentation.Models;

public class ProjectHourModel
{
    public Guid Uuid { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan Duration { get; set; }
    public UserModel User { get; set; }
}