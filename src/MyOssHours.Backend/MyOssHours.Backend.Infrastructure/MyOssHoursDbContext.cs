using Microsoft.EntityFrameworkCore;
using MyOssHours.Backend.Infrastructure.Model;

namespace MyOssHours.Backend.Infrastructure;

internal class MyOssHoursDbContext : DbContext
{
    public MyOssHoursDbContext(DbContextOptions<MyOssHoursDbContext> options) : base(options)
    {
    }

    public required DbSet<ProjectEntity> Projects { get; set; }
    public required DbSet<ProjectHourEntity> ProjectHours { get; set; }
    public required DbSet<ProjectMemberEntity> ProjectMembers { get; set; }
    public required DbSet<WorkItemEntity> WorkItems { get; set; }
    public required DbSet<UserEntity> Users { get; set; }
}