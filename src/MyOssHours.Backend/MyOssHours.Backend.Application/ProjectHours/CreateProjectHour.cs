using MediatR;
using MyOssHours.Backend.Application.Abstractions;
using MyOssHours.Backend.Domain.Entities;
using MyOssHours.Backend.Domain.Enumerations;
using MyOssHours.Backend.Domain.ValueObjects;

namespace MyOssHours.Backend.Application.ProjectHours;

public static class CreateProjectHour
{
    public class Handler : IRequestHandler<Command, Response>
    {
        private readonly IProjectHoursRepository _repository;

        public Handler(IProjectHoursRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
        {
            var projectHour = await _repository.CreateProjectHour(ProjectHour.Create(
                request.Project, request.User, request.Date, request.Duration, request.Description));

            return new Response
            {
                ProjectHour = projectHour
            };
        }
    }

    public class Command : IRequest<Response>
    {
        public ProjectId Project { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Duration { get; set; }
        public UserId User { get; set; }
        public string Description { get; set; }
    }

    public class Response
    {
        public required ProjectHour ProjectHour { get; set; }
    }
}