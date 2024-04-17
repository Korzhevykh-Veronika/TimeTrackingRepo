namespace VeronikaKorzhevykh.TimeTrackingTask.Core.Models;

public class Project
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public IEnumerable<WorkTime>? WorkTimes { get; set; }
}
