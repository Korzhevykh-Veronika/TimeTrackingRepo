using Microsoft.EntityFrameworkCore;
using VeronikaKorzhevykh.TimeTrackingTask.Context;
using VeronikaKorzhevykh.TimeTrackingTask.Core.Models;
using VeronikaKorzhevykh.TimeTrackingTask.DataAccess.Repositories.Interface;

namespace VeronikaKorzhevykh.TimeTrackingTask.DataAccess.Repositories.Implementation;

public class WorkTimeRepository : EntityFrameworkRepository<WorkTime>, IWorkTimeRepository
{
    public WorkTimeRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<WorkTime> GetOpenWorkTimeAsync(Guid userId)
    {
        return await _dbSet
            .Include(w => w.Project)
            .FirstOrDefaultAsync(w => w.UserId == userId && w.ClockOutTime == null);
    }

    public async Task<List<WorkTime>> GetAllByUserId(Guid userId)
    {
        return await _dbSet
            .Where(w => w.UserId == userId)
            .Include(w => w.Project)
            .ToListAsync();
    }
}
