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
                request.WorkItem, request.User, request.Date, request.Duration, request.Description));

            return new Response
            {
                ProjectHour = projectHour
            };
        }
    }

    public class Command : IRequest<Response>
    {
        public required WorkItemId WorkItem { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Duration { get; set; }
        public required UserId User { get; set; }
        public required string Description { get; set; }
    }

    public class Response
    {
        public required ProjectHour ProjectHour { get; set; }
    }
}