using Microsoft.EntityFrameworkCore;
using MyOssHours.Backend.Application.Abstractions;
using MyOssHours.Backend.Domain.Entities;
using MyOssHours.Backend.Infrastructure.Model;

namespace MyOssHours.Backend.Infrastructure.Repositories;

internal class ProjectsRepository : IProjectsRepository
{
    private readonly MyOssHoursDbContext _dbContext;

    public ProjectsRepository(MyOssHoursDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Project>> GetProjects(PaginationQuery query)
    {
        var projects = await _dbContext.Projects
            .Skip(query.Offset)
            .Take(query.Size)
            .Select(x =>
                Project.Create(x.Uuid, x.Name, x.Description,
                    x.Members.Select(y => ProjectMember.Create(y.User.Uuid, y.Role)),
                    Array.Empty<ProjectHour>()))
            .ToListAsync();

        return projects;
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