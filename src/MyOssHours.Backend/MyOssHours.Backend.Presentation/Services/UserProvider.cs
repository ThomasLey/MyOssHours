using MyOssHours.Backend.Application.Abstractions;
using MyOssHours.Backend.Domain.Entities;

namespace MyOssHours.Backend.Presentation.Services;

public class UserProvider : IUserProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    public User GetCurrentUser()
    {
        return _httpContextAccessor.HttpContext.Items["User"] as User;
    }
}