using VeronikaKorzhevykh.TimeTrackingTask.Application;
using VeronikaKorzhevykh.TimeTrackingTask.DataAccess;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureBLLServices();
builder.Services.ConfigureDALServices();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
