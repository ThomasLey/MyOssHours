using AutoMapper;
using MyOssHours.Backend.Domain.Entities;
using MyOssHours.Backend.Presentation.Models;

namespace MyOssHours.Backend.Presentation;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserModel>()
            .ForMember(dst => dst.Nickname, opt => opt.MapFrom(src => src.Nickname))
            .ForMember(dst => dst.Uuid, opt => opt.MapFrom(src => src.Uuid));

        CreateMap<Project, ProjectModel>()
            .ForPath(dst => dst.WorkItems, c => c.MapFrom(src => src.WorkItems))
            .ForPath(dst => dst.Members, c => c.MapFrom(src => src.ProjectMembers));

        CreateMap<WorkItem, WorkItemModel>()
            .ForPath(dst => dst.Hours, c => c.MapFrom(src => src.ProjectHours));
        CreateMap<ProjectHour, ProjectHourModel>()

            .ForMember(dst => dst.DurationInMinutes, opt => opt.MapFrom(src => src.Duration.TotalMinutes))
            .ForMember(dst => dst.User, opt => opt.MapFrom(src => src.User.Uuid))
            .ForMember(dst => dst.Date, opt => opt.MapFrom(src => src.StartDate));

        CreateMap<ProjectMember, ProjectMemberModel>()
            .ForMember(dst => dst.UserId, opt => opt.MapFrom(src => src.UserId.Uuid));
    }
}