using MyOssHours.Backend.Domain.Enumerations;
using MyOssHours.Backend.Domain.ValueObjects;

namespace MyOssHours.Backend.Domain.Entities;

/// <summary>
///     This is the domain model for the project
/// </summary>
public class Project
{
    private Project(ProjectId uuid, string name, string description)
    {
        Uuid = uuid;
        Name = name;
        Description = description;
        ProjectHours = new List<ProjectHour>();
        ProjectMembers = new List<ProjectMember>();
    }

    public ProjectId Uuid { get; }

    public string Name { get; }
    public string Description { get; }
    public IEnumerable<ProjectHour> ProjectHours { get; private set; }
    public IEnumerable<ProjectMember> ProjectMembers { get; private set; }

    public static Project Create(string name, string description, IEnumerable<ProjectMember> members,
        IEnumerable<ProjectHour>? projectHours = null)
    {
        return Create(new ProjectId(), name, description, members, projectHours);
    }

    public static Project Create(ProjectId id, string name, string description, IEnumerable<ProjectMember> members,
        IEnumerable<ProjectHour>? projectHours = null)
    {
        var projectMembers = members as ProjectMember[] ?? members.ToArray();
        if (projectMembers.FirstOrDefault(x => x.Role == PermissionLevel.Owner) == null)
            throw new ArgumentException("At least one owner is required");

        return new Project(new ProjectId(), name, description)
        {
            ProjectMembers = projectMembers.ToArray(),
            ProjectHours = projectHours ?? Array.Empty<ProjectHour>()
        };
    }
}