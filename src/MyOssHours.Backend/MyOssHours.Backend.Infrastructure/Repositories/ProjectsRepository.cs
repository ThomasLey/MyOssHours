using Microsoft.EntityFrameworkCore;
using MyOssHours.Backend.Application.Abstractions;
using MyOssHours.Backend.Domain.Entities;
using MyOssHours.Backend.Domain.Enumerations;
using MyOssHours.Backend.Domain.ValueObjects;
using MyOssHours.Backend.Infrastructure.Model;

namespace MyOssHours.Backend.Infrastructure.Repositories;

internal class ProjectsRepository : IProjectsRepository
{
    private readonly MyOssHoursDbContext _dbContext;

    public ProjectsRepository(MyOssHoursDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Project>> GetProjects(UserId currentUser, int offset = 0, int size = 20)
    {
        var projects = await _dbContext.Projects
            .Where(x => x.Members.Any(m => m.User.Uuid == currentUser.Uuid && m.Role > PermissionLevel.None))
            .Skip(offset)
            .Take(size)
            .Select(x =>
                Project.Create(x.Uuid, x.Name, x.Description, Array.Empty<ProjectMember>(), Array.Empty<WorkItem>()))
            .ToListAsync();

        return projects;
    }

    public async Task<Project> GetProject(Guid uuid, UserId currentUser)
    {
        var project = await _dbContext.Projects
            .Include(x => x.Members)
            .ThenInclude(x => x.User)
            .Include(x => x.WorkItems)
            .ThenInclude(x => x.Hours)
            .FirstOrDefaultAsync(x => x.Uuid == uuid && x.Members.Any(m => m.User.Uuid == currentUser.Uuid && m.Role > PermissionLevel.None));
        return Project.Create(project.Uuid, project.Name, project.Description,
                       project.Members.Select(x => ProjectMember.Create(x.User.Uuid, x.Role)),
                       project.WorkItems.Select(x => WorkItem.Create(new WorkItemId(x.Uuid), project.Uuid, x.Name, x.Description,
                           x.Hours.Select(y => ProjectHour.Create(new ProjectHourId(y.Uuid), project.Uuid, y.User.Uuid, y.StartDate, y.Duration, y.Description))))
                       );
    }

    public async Task<Project> CreateProject(Project project)
    {
        var projectEntity = new ProjectEntity
        {
            Uuid = project.Uuid,
            Name = project.Name,
            Description = project.Description
        };
        // add members
        foreach (var member in project.ProjectMembers)
            projectEntity.Members.Add(new ProjectMemberEntity
            {
                User = _dbContext.Users.First(x => x.Uuid == member.UserId),
                Role = member.Role
            });
        _dbContext.Projects.Add(projectEntity);
        await _dbContext.SaveChangesAsync();
        return project;
    }
}

// User Repository