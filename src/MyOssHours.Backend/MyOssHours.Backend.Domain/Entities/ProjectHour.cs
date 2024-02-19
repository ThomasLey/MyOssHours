using MyOssHours.Backend.Domain.ValueObjects;

namespace MyOssHours.Backend.Domain.Entities;

public class ProjectHour
{
    private ProjectHour(ProjectHourId id, ProjectId project, UserId user, DateTime startDate, decimal hours)
    {
        Uuid = id;
        Project = project;
        User = user;
        StartDate = startDate;
        Hours = hours;
    }

    public decimal Hours { get; }

    public DateTime StartDate { get; }

    public UserId User { get; }

    public ProjectHourId Uuid { get; }

    public ProjectId Project { get; }

    public static ProjectHour Create(ProjectHourId uuid, ProjectId project, UserId user, DateTime startDate,
        decimal hours)
    {
        return new ProjectHour(uuid, project, user, startDate, hours);
    }

    public static ProjectHour Create(ProjectId project, UserId user, DateTime startDate, decimal hours)
    {
        return new ProjectHour(new ProjectHourId(), project, user, startDate, hours);
    }
}