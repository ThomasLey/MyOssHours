using MyOssHours.Backend.Domain.Entities;

namespace MyOssHours.Backend.Application.Abstractions;

public interface IProjectsRepository
{
    Task<IEnumerable<Project>> GetProjects(int offset = 0, int size = 20);
    Task<Project> GetProject(Guid uuid);
    Task<Project> CreateProject(Project project);
}