using VeronikaKorzhevykh.TimeTrackingTask.Core.Models;
using VeronikaKorzhevykh.TimeTrackingTask.DataAccess.Repositories.Interface;
using VeronikaKorzhevykh.TimeTrackingTask.Application.Services.Interfaces;

namespace VeronikaKorzhevykh.TimeTrackingTask.Application.Services.Implementation;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;

    public ProjectService(
        IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }
    public async Task CreateAsync(Project project)
    {
        project.Id = Guid.NewGuid();
        await _projectRepository.AddAsync(project);
    }

    public async Task DeleteAsync(Guid id)
    {
        var project = await _projectRepository.GetByIdAsync(id);
        await _projectRepository.DeleteAsync(project);
    }

    public Task<IEnumerable<Project>> GetAllAsync()
    {
        return _projectRepository.GetAllAsync();
    }

    public async Task<Project> GetAsync(Guid id)
    {
        var project = await _projectRepository.GetByIdAsync(id);
        return project;
    }

    public async Task UpdateAsync(Project project)
    {
        await _projectRepository.UpdateAsync(project);
    }
}
