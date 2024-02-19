namespace MyOssHours.Backend.Application.Dto;

public class ProjectDto
{
    public Guid Uuid { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public IEnumerable<ProjectHourDto> ProjectHours { get; set; }
}