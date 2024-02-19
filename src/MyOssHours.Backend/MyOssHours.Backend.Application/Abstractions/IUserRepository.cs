using MyOssHours.Backend.Domain.Entities;

namespace MyOssHours.Backend.Application.Abstractions;

public interface IUserRepository
{
    Task<User> EnsureUser(string sid, string nickname, string email);
}