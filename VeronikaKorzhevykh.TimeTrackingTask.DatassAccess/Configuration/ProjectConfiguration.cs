using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VeronikaKorzhevykh.TimeTrackingTask.Core.Models;

namespace VeronikaKorzhevykh.TimeTrackingTask.Configuration;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder
            .ToTable("Projects")
            .HasKey(x => x.Id);

        builder
            .Property(e => e.Title)
            .HasMaxLength(250)
            .IsRequired();

        builder.HasIndex(e => e.Title).IsUnique();

        builder
           .Property(e => e.Description)
           .IsRequired(false);

        builder
            .HasMany(e => e.WorkTimes)
            .WithOne(w => w.Project)
            .HasForeignKey(e => e.ProjectId)
            .IsRequired(false);
    }
}