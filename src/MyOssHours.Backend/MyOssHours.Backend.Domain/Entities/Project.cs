﻿using MyOssHours.Backend.Domain.Enumerations;
using MyOssHours.Backend.Domain.ValueObjects;

namespace MyOssHours.Backend.Domain.Entities;

// WorkItem Domain Model which has WorkItems

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
        WorkItems = new List<WorkItem>();
        ProjectMembers = new List<ProjectMember>();
    }

    public ProjectId Uuid { get; }

    public string Name { get; }
    public string Description { get; }
    public IEnumerable<WorkItem> WorkItems { get; private set; }
    public IEnumerable<ProjectMember> ProjectMembers { get; private set; }

    public static Project Create(
        string name, string description,
        IEnumerable<ProjectMember> members,
        IEnumerable<WorkItem>? workItems = null)
    {
        return Create(new ProjectId(), name, description, members, workItems);
    }

    public static Project Create(
        ProjectId id, string name, string description,
        IEnumerable<ProjectMember> members,
        IEnumerable<WorkItem>? workItems = null)
    {
        var projectMembers = members as ProjectMember[] ?? members.ToArray();
        //if (projectMembers.FirstOrDefault(x => x.Role == PermissionLevel.Owner) == null)
        //    throw new ArgumentException("At least one owner is required");

        return new Project(id, name, description)
        {
            ProjectMembers = projectMembers.ToArray(),
            WorkItems = workItems ?? Array.Empty<WorkItem>()
        };
    }
}