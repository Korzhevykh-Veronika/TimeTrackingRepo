using VeronikaKorzhevykh.TimeTrackingTask.Context;
using VeronikaKorzhevykh.TimeTrackingTask.Core.Models;
using VeronikaKorzhevykh.TimeTrackingTask.DataAccess.Repositories.Interface;

namespace VeronikaKorzhevykh.TimeTrackingTask.DataAccess.Repositories.Implementation;

public class ProjectRepository : EntityFrameworkRepository<Project>, IProjectRepository
{
    public ProjectRepository(AppDbContext context) : base(context)
    {
    }
}
