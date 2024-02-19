
namespace MyOssHours.Backend.Presentation.Models;

public class ProjectModel
{
    public Guid Uuid { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<ProjectMemberModel> Members { get; set; } = new List<ProjectMemberModel>();
    public List<WorkItemModel> WorkItems { get; set; } = new List<WorkItemModel>();
}