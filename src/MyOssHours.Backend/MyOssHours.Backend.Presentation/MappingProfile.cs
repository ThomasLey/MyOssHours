using AutoMapper;
using MyOssHours.Backend.Domain.Entities;
using MyOssHours.Backend.Presentation.Models;

namespace MyOssHours.Backend.Presentation;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserModel>();
        CreateMap<Project, ProjectModel>()
            .ForPath(x => x.Hours, c => c.MapFrom(x => x.ProjectHours))
            .ForPath(x => x.Members, c => c.MapFrom(x => x.ProjectMembers));
        CreateMap<ProjectHour, ProjectHourModel>()
            .ForMember(x=>x.DurationInMinutes, y=> y.MapFrom(z=>z.Duration.TotalMinutes))
            .ForMember(x => x.User, y => y.MapFrom(z => z.User.Uuid))
            .ForMember(x => x.Date, y => y.MapFrom(z => z.StartDate));
        CreateMap<ProjectMember, ProjectMemberModel>()
            .ForMember(x=> x.UserId, y=>y.MapFrom(z=>z.UserId.Uuid));
    }
}