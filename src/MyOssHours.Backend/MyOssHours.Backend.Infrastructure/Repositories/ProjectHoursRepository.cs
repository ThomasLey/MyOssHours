using MyOssHours.Backend.Application.Abstractions;
using MyOssHours.Backend.Domain.Entities;
using MyOssHours.Backend.Infrastructure.Model;

namespace MyOssHours.Backend.Infrastructure.Repositories;

internal class ProjectHoursRepository : IProjectHoursRepository
{
    private readonly MyOssHoursDbContext _dbContext;

    public ProjectHoursRepository(MyOssHoursDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ProjectHour> CreateProjectHour(ProjectHour projectHour)
    {
        var p = _dbContext.Projects.First(x => x.Uuid == projectHour.Project);
        var u = _dbContext.Users.First(x => x.Uuid == projectHour.User);
        var ph = new ProjectHourEntity
        {
            Uuid = projectHour.Uuid,
            Project = p,
            User = u,
            StartDate = projectHour.StartDate,
            Duration = projectHour.Duration,
            Description = projectHour.Description
        };
        _dbContext.ProjectHours.Add(ph);
        await _dbContext.SaveChangesAsync();
        return ProjectHour.Create(ph.Uuid, ph.Project.Uuid, ph.User.Uuid, ph.StartDate, ph.Duration, ph.Description);
    }
}