using MediatR;
using MyOssHours.Backend.Application.Abstractions;
using MyOssHours.Backend.Domain.Entities;

namespace MyOssHours.Backend.Application.Projects;

public static class GetProject
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
            var project = await _repository.GetProject(request.Uuid);

            return new Response
            {
                Project = project
            };
        }
    }

    public class Query : IRequest<Response>
    {
        public Guid Uuid { get; set; }
    }

    public class Response
    {
        public required Project Project { get; set; }
    }
}