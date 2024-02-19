using AutoMapper;
using MyOssHours.Backend.Domain.Entities;

namespace MyOssHours.Backend.Application.Dto;

public class DtoMapperProfile : Profile
{
    public DtoMapperProfile()
    {
        CreateMap<Project, ProjectDto>();
        CreateMap<ProjectHour, ProjectHourDto>();
    }
}