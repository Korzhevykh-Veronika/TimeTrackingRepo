using VeronikaKorzhevykh.TimeTrackingTask.Core.Models;

namespace VeronikaKorzhevykh.TimeTrackingTask.Application.Services.Interfaces;

public interface IWorkTimeService
{
    Task ClockInAsync(Guid userId, Guid projectId);
    Task ClockOutAsync(Guid userId);
    Task<IEnumerable<WorkTime>> GetWorkHistoryAsync(Guid userId);
    Task<WorkTime> GetOpenWorkTimeAsync(Guid userId);
}
