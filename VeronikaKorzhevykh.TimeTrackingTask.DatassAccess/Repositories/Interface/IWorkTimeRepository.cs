using VeronikaKorzhevykh.TimeTrackingTask.Core.Models;

namespace VeronikaKorzhevykh.TimeTrackingTask.DataAccess.Repositories.Interface;

public interface IWorkTimeRepository : IRepository<WorkTime>
{
    Task<WorkTime> GetOpenWorkTimeAsync(Guid employeeId);
    Task<List<WorkTime>> GetAllByUserId(Guid employeeId);
}
