using MyOssHours.Backend.Domain.Entities;

namespace MyOssHours.Backend.Application.Abstractions;

public interface IProjectHoursRepository
{
    Task<ProjectHour> CreateProjectHour(ProjectHour projectHour);
}