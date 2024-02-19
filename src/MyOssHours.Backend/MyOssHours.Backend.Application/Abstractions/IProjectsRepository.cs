using MyOssHours.Backend.Domain.Entities;

namespace MyOssHours.Backend.Application.Abstractions;

public interface IProjectsRepository
{
    Task<IEnumerable<Project>> GetProjects(PaginationQuery query);
    Task<Project> CreateProject(Project project);
}