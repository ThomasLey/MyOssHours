using MyOssHours.Backend.Domain.Entities;

namespace MyOssHours.Backend.Application.Abstractions;

public interface IWorkItemsRepository
{
    Task<WorkItem> CreateWorkItem(WorkItem workItem);
}