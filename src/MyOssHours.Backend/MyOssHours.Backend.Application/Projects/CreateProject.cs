using AutoMapper;
using MediatR;
using MyOssHours.Backend.Application.Abstractions;
using MyOssHours.Backend.Application.Dto;
using MyOssHours.Backend.Domain.Entities;
using MyOssHours.Backend.Domain.Enumerations;
using MyOssHours.Backend.Domain.ValueObjects;

namespace MyOssHours.Backend.Application.Projects;

public static class CreateProject
{
    public class Handler : IRequestHandler<Command, Response>
    {
        private readonly IMapper _mapper;
        private readonly IProjectsRepository _repository;

        public Handler(IProjectsRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
        {
            var project = await _repository.CreateProject(Project.Create(request.Name, request.Description,
                new[] { new ProjectMember(request.Owner, PermissionLevel.Owner) }));
            var projectDto = _mapper.Map<ProjectDto>(project);

            return new Response
            {
                Project = projectDto
            };
        }
    }

    public class Command : IRequest<Response>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public UserId Owner { get; set; }
    }

    public class Response
    {
        public required ProjectDto Project { get; set; }
    }
}