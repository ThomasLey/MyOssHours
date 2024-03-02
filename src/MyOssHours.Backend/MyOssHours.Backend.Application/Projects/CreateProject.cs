﻿using MediatR;
using MyOssHours.Backend.Application.Abstractions;
using MyOssHours.Backend.Domain.Entities;
using MyOssHours.Backend.Domain.Enumerations;
using MyOssHours.Backend.Domain.ValueObjects;

namespace MyOssHours.Backend.Application.Projects;

public static class CreateProject
{
    public class Handler : IRequestHandler<Command, Response>
    {
        private readonly IProjectsRepository _repository;

        public Handler(IProjectsRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
        {
            var project = await _repository.CreateProject(Project.Create(request.Name, request.Description,
                new[] { ProjectMember.Create(request.Owner.Uuid, PermissionLevel.Owner) }));

            return new Response
            {
                Project = project
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
        public required Project Project { get; set; }
    }
}