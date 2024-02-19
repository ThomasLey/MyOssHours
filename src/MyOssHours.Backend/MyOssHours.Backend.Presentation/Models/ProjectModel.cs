namespace MyOssHours.Backend.Presentation.Models;

public class ProjectModel
{
    public Guid Uuid { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public IEnumerable<ProjectMemberModel> Members { get; set; }
    public IEnumerable<ProjectHourModel> Hours { get; set; }
}