using MyOssHours.Backend.Domain.Entities;
using MyOssHours.Backend.Domain.ValueObjects;

namespace MyOssHours.Backend.Application.Abstractions;

public interface IWorkItemsRepository
{
    Task<WorkItem> CreateWorkItem(WorkItem workItem);
    Task<IEnumerable<WorkItem>> GetWorkItems(UserId uuid, ProjectId project);
    Task<bool> DeleteWorkItem(UserId uuid, WorkItemId workitem);
}