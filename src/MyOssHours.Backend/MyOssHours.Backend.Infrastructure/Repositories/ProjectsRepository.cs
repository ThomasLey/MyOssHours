using Microsoft.EntityFrameworkCore;
using MyOssHours.Backend.Application.Abstractions;
using MyOssHours.Backend.Domain.Entities;
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

    public async Task<IEnumerable<Project>> GetProjects(int offset = 0, int size = 20)
    {
        var projects = await _dbContext.Projects
            .Skip(offset)
            .Take(size)
            .Select(x =>
                Project.Create(x.Uuid, x.Name, x.Description,
                    x.Members.Select(y => ProjectMember.Create(y.User.Uuid, y.Role)),
                    Array.Empty<ProjectHour>()))
            .ToListAsync();

        return projects;
    }

    public async Task<Project> GetProject(Guid uuid)
    {
        var project = await _dbContext.Projects
            .Include(x => x.Members)
            .ThenInclude(x => x.User)
            .Include(x => x.Hours)
            .FirstOrDefaultAsync(x => x.Uuid == uuid);
        return Project.Create(project.Uuid, project.Name, project.Description,
                       project.Members.Select(x => ProjectMember.Create(x.User.Uuid, x.Role)),
                                  project.Hours.Select(x => ProjectHour.Create( new ProjectHourId(x.Uuid), project.Uuid, x.User.Uuid, x.StartDate, x.Duration, x.Description)));
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