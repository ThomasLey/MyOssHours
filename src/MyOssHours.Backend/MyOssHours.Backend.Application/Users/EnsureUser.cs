using AutoMapper;
using MediatR;
using MyOssHours.Backend.Application.Abstractions;
using MyOssHours.Backend.Domain.Entities;

namespace MyOssHours.Backend.Application.Users;

public class EnsureUser
{
    public class Handler : IRequestHandler<Command, Response>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _repository;

        public Handler(IUserRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Response> Handle(Command command, CancellationToken cancellationToken)
        {
            var user = await _repository.EnsureUser(command.Sid, command.Nickname, command.Email);
            var userDto = _mapper.Map<User>(user);

            return new Response
            {
                User = userDto
            };
        }
    }

    public class Command : IRequest<Response>
    {
        public required string Sid { get; set; }
        public required string Email { get; set; }
        public required string Nickname { get; set; }
    }

    public class Response
    {
        public required User User { get; set; }
    }
}