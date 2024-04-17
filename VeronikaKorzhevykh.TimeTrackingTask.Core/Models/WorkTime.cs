namespace VeronikaKorzhevykh.TimeTrackingTask.Core.Models;

public class WorkTime
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime ClockInTime { get; set; }
    public DateTime? ClockOutTime { get; set; }

    public Guid ProjectId { get; set; }
    public Project Project { get; set; }
}
