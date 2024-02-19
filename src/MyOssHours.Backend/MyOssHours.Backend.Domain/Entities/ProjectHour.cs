using MyOssHours.Backend.Domain.ValueObjects;
using System;

namespace MyOssHours.Backend.Domain.Entities;

public class ProjectHour
{
    private ProjectHour(ProjectHourId id, ProjectId project, UserId user, DateTime startDate, TimeSpan duration, string description)
    {
        Uuid = id;
        Project = project;
        User = user;
        StartDate = startDate;
        Duration = duration;
        Description = description;
    }

    public TimeSpan Duration { get; }
    public string Description { get; }

    public DateTime StartDate { get; }

    public UserId User { get; }

    public ProjectHourId Uuid { get; }

    public ProjectId Project { get; }

    public static ProjectHour Create(ProjectHourId uuid, ProjectId project, UserId user, DateTime startDate, TimeSpan duration, string description)
    {
        return new ProjectHour(uuid, project, user, startDate, duration, description);
    }

    public static ProjectHour Create(ProjectId project, UserId user, DateTime startDate, TimeSpan duration, string description)
    {
        return Create(new ProjectHourId(), project, user, startDate, duration, description);
    }
}