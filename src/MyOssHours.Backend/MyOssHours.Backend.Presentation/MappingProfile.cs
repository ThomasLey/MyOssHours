using AutoMapper;
using MyOssHours.Backend.Application.Dto;
using MyOssHours.Backend.Presentation.Models;

namespace MyOssHours.Backend.Presentation;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ProjectDto, ProjectModel>();
    }
}