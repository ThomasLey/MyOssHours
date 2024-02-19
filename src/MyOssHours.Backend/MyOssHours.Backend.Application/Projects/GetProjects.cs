﻿using MediatR;
using MyOssHours.Backend.Application.Abstractions;
using MyOssHours.Backend.Domain.Entities;

namespace MyOssHours.Backend.Application.Projects;

public static class GetProjects
{
    public class Handler : IRequestHandler<Query, Response>
    {
        private readonly IProjectsRepository _repository;

        public Handler(IProjectsRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
        {
            var projects = await _repository.GetProjects(request.Offset, request.Size);

            return new Response
            {
                Projects = projects
            };
        }
    }

    public class Query : IRequest<Response>
    {
        public int Offset { get; set; }
        public int Size { get; set; }
    }

    public class Response
    {
        public IEnumerable<Project> Projects { get; set; }
    }
}