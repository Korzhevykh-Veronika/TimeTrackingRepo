using VeronikaKorzhevykh.TimeTrackingTask.Core.Models;
using VeronikaKorzhevykh.TimeTrackingTask.DataAccess.Repositories.Interface;
using VeronikaKorzhevykh.TimeTrackingTask.Application.Services.Interfaces;

namespace VeronikaKorzhevykh.TimeTrackingTask.Application.Services.Implementation;

public class WorkTimeService : IWorkTimeService
{
    private readonly IWorkTimeRepository _workTimeRepository;
    private readonly IProjectRepository _projectRepository;

    public WorkTimeService(
        IWorkTimeRepository workTimeRepository,
        IProjectRepository projectRepository)
    {
        _workTimeRepository = workTimeRepository;
        _projectRepository = projectRepository;
    }

    public async Task ClockInAsync(Guid userId, Guid projectId)
    {
        var openWorkTime = await _workTimeRepository.GetOpenWorkTimeAsync(userId);

        if (openWorkTime != null)
        {
            throw new Exception ($"WorkTime has been already created.");
        }

        var project = await _projectRepository.GetByIdAsync(projectId);

        var newWorkTime = new WorkTime
        {
            ClockInTime = DateTime.Now,
            ClockOutTime = null,
            Project = project,
            UserId = userId
        };

        await _workTimeRepository.AddAsync(newWorkTime);
    }

    public async Task ClockOutAsync(Guid userId)
    {
        var openWorkTime = await _workTimeRepository.GetOpenWorkTimeAsync(userId);

        if (openWorkTime == null)
        {
            throw new Exception($"WorkTime has not been created.");
        }

        openWorkTime.ClockOutTime = DateTime.Now;

        await _workTimeRepository.UpdateAsync(openWorkTime);
    }

    public async Task<WorkTime> GetOpenWorkTimeAsync(Guid userId)
    {
        var openWorkTime = await _workTimeRepository.GetOpenWorkTimeAsync(userId);

        return openWorkTime;
    }

    public async Task<IEnumerable<WorkTime>> GetWorkHistoryAsync(Guid userId)
    {
        return await _workTimeRepository.GetAllByUserId(userId);
    }
}
