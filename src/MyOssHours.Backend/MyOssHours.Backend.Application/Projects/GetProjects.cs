using AutoMapper;
using MediatR;
using MyOssHours.Backend.Application.Abstractions;
using MyOssHours.Backend.Application.Dto;

namespace MyOssHours.Backend.Application.Projects;

public static class GetProjects
{
    public class Handler : IRequestHandler<Query, Response>
    {
        private readonly IMapper _mapper;
        private readonly IProjectsRepository _repository;

        public Handler(IProjectsRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
        {
            var projects = await _repository.GetProjects(request);
            var projectsDto = _mapper.Map<IEnumerable<ProjectDto>>(projects);

            return new Response
            {
                Projects = projectsDto
            };
        }
    }

    public class Query : PaginationQuery, IRequest<Response>
    {
    }

    public class Response
    {
        public IEnumerable<ProjectDto> Projects { get; set; }
    }
}