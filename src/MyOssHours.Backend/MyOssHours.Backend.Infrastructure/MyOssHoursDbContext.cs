using Microsoft.EntityFrameworkCore;
using MyOssHours.Backend.Infrastructure.Model;

namespace MyOssHours.Backend.Infrastructure;

internal class MyOssHoursDbContext : DbContext
{
    public MyOssHoursDbContext(DbContextOptions<MyOssHoursDbContext> options) : base(options)
    {
    }

    public DbSet<ProjectEntity> Projects { get; set; }
    public DbSet<ProjectHourEntity> ProjectHours { get; set; }
    public DbSet<UserEntity> Users { get; set; }
}