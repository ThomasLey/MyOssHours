using AutoMapper;
using MyOssHours.Backend.Domain.Entities;
using MyOssHours.Backend.Presentation.Models;

namespace MyOssHours.Backend.Presentation;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Project, ProjectModel>();
    }
}