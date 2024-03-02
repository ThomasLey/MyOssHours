using Microsoft.EntityFrameworkCore;
using MyOssHours.Backend.Application.Abstractions;
using MyOssHours.Backend.Domain.Entities;
using MyOssHours.Backend.Domain.ValueObjects;
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
        var w = _dbContext.WorkItems.First(x => x.Uuid == projectHour.WorkItem);
        var u = _dbContext.Users.First(x => x.Uuid == projectHour.User);
        var ph = new ProjectHourEntity
        {
            Uuid = projectHour.Uuid,
            WorkItem = w,
            User = u,
            StartDate = projectHour.StartDate,
            Duration = projectHour.Duration,
            Description = projectHour.Description
        };
        _dbContext.ProjectHours.Add(ph);
        await _dbContext.SaveChangesAsync();
        return ProjectHour.Create(ph.Uuid, ph.WorkItem.Uuid, ph.User.Uuid, ph.StartDate, ph.Duration, ph.Description);
    }
}
internal class WorkItemsRepository : IWorkItemsRepository
{
    private readonly MyOssHoursDbContext _dbContext;

    public WorkItemsRepository(MyOssHoursDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<WorkItem> CreateWorkItem(WorkItem workItem)
    {
        var p = _dbContext.Projects.First(x => x.Uuid == workItem.Project);
        var ph = new WorkItemEntity
        {
            Uuid = workItem.Uuid,
            Name = workItem.Name,
            Description = workItem.Description,
            Project = p
        };
        _dbContext.WorkItems.Add(ph);
        await _dbContext.SaveChangesAsync();
        return WorkItem.Create(ph.Uuid, ph.Project.Uuid, ph.Name, ph.Description);
    }

    public async Task<IEnumerable<WorkItem>> GetWorkItems(UserId uuid, ProjectId project)
    {
        var p = _dbContext.Projects.Include(x => x.Members).ThenInclude(x => x.User).First(x => x.Uuid == project);
        var hasAccess = p.Members.Any(x => x.User.Uuid == uuid);
        if (!hasAccess)
            throw new UnauthorizedAccessException();

        var workItems = _dbContext.WorkItems.Where(x => x.Project.Uuid == project).Select(x => WorkItem.Create(x.Uuid, x.Project.Uuid, x.Name, x.Description, new ProjectHour[]{}));
        return workItems;
    }

    public Task<bool> DeleteWorkItem(UserId uuid, WorkItemId workitem)
    {
        throw new NotImplementedException();
    }
}