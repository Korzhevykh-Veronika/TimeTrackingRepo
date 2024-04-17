using Microsoft.Extensions.DependencyInjection;
using VeronikaKorzhevykh.TimeTrackingTask.Application.Services.Implementation;
using VeronikaKorzhevykh.TimeTrackingTask.Application.Services.Interfaces;

namespace VeronikaKorzhevykh.TimeTrackingTask.Application;

public static class Services2
{
    public static void ConfigureBLLServices(this IServiceCollection services)
    {
        services.AddTransient<IProjectService, ProjectService>();
        services.AddTransient<IWorkTimeService, WorkTimeService>();
    }
}
