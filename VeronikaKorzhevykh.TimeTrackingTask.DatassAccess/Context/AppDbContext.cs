using Microsoft.EntityFrameworkCore;
using VeronikaKorzhevykh.TimeTrackingTask.Configuration;
using VeronikaKorzhevykh.TimeTrackingTask.Core.Models;

namespace VeronikaKorzhevykh.TimeTrackingTask.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<WorkTime> WorkHistories { get; set; }
    public DbSet<Project> Projects { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProjectConfiguration());
        modelBuilder.ApplyConfiguration(new WorkTimeConfiguration());
    }
}
