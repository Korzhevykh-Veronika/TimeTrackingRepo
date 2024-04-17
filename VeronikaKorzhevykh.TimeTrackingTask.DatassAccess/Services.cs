using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VeronikaKorzhevykh.TimeTrackingTask.Context;
using VeronikaKorzhevykh.TimeTrackingTask.DataAccess.Repositories.Implementation;
using VeronikaKorzhevykh.TimeTrackingTask.DataAccess.Repositories.Interface;

namespace VeronikaKorzhevykh.TimeTrackingTask.DataAccess;

public static class Services
{
    public static void ConfigureDALServices(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            //for easy testing
            //options.UseInMemoryDatabase(databaseName: "TimeTrackingDB");
            //return;

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            options.UseSqlServer(connectionString, sqlServerOptions =>
            {
                sqlServerOptions.EnableRetryOnFailure();
            });
        });

        services.AddTransient<IProjectRepository, ProjectRepository>();
        services.AddTransient<IWorkTimeRepository, WorkTimeRepository>();
    }
}
