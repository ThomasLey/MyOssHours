namespace MyOssHours.Backend.Presentation.Controllers;

public class CreateProjectHourCommand
{
    public Guid Project { get; set; }
    public DateTime Date { get; set; }
    public int DurationInMinutes { get; set; }
    public Guid User { get; set; }
    public string Description { get; set; }
}