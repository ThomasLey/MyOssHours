namespace MyOssHours.Backend.Application.Dto;

public class ProjectHourDto
{
    public Guid Uuid { get; set; }
    public DateTime Date { get; set; }
    public decimal Hours { get; set; }
    public string? Comment { get; set; }
}