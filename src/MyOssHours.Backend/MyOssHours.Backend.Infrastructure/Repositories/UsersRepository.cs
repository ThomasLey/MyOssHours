using Microsoft.EntityFrameworkCore;
using MyOssHours.Backend.Application.Abstractions;
using MyOssHours.Backend.Domain.Entities;
using MyOssHours.Backend.Infrastructure.Model;

namespace MyOssHours.Backend.Infrastructure.Repositories;

internal class UsersRepository : IUserRepository
{
    private readonly MyOssHoursDbContext _dbContext;

    public UsersRepository(MyOssHoursDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User> EnsureUser(string sid, string nickname, string email)
    {
        // find user in database and return if available
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Sid == sid);
        if (user != null) return User.Create(user.Uuid, user.Nickname, user.Email);

        // create user
        user = new UserEntity
        {
            Uuid = Guid.NewGuid(),
            Sid = sid,
            Nickname = nickname,
            Email = email
        };
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();

        // return user
        return User.Create(user.Uuid, user.Nickname, user.Email);
    }
}