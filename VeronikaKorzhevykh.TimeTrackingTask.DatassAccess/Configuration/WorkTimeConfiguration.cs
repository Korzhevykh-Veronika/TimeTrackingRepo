using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VeronikaKorzhevykh.TimeTrackingTask.Core.Models;

namespace VeronikaKorzhevykh.TimeTrackingTask.Configuration;

public class WorkTimeConfiguration : IEntityTypeConfiguration<WorkTime>
{
    public void Configure(EntityTypeBuilder<WorkTime> builder)
    {
        builder
            .ToTable("WorkTimes")
            .HasKey(x => x.Id);

        builder
            .Property(e => e.ClockInTime)
            .IsRequired(true);

        builder
           .Property(e => e.ClockOutTime)
           .IsRequired(false);

        builder
            .HasOne(e => e.Project)
            .WithMany(w => w.WorkTimes)
            .HasForeignKey(e => e.ProjectId)
            .IsRequired(true);
    }
}