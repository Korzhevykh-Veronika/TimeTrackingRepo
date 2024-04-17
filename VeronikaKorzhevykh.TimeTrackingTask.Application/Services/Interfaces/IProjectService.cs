using VeronikaKorzhevykh.TimeTrackingTask.Core.Models;

namespace VeronikaKorzhevykh.TimeTrackingTask.Application.Services.Interfaces;

public interface IProjectService
{
    Task CreateAsync(Project project);
    Task<Project> GetAsync(Guid id);
    Task UpdateAsync(Project project);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<Project>> GetAllAsync();
}
