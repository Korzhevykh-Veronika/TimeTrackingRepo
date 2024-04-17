using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VeronikaKorzhevykh.TimeTrackingTask.Application.Services.Interfaces;
using VeronikaKorzhevykh.TimeTrackingTask.ViewModels;

namespace VeronikaKorzhevykh.TimeTrackingTask.Controllers;

public class HomeController : Controller
{
    private readonly IWorkTimeService _workTimeService;
    private readonly IProjectService _projectService;

    public HomeController(
        IWorkTimeService workTimeService,
        IProjectService projectService)
    {
        _workTimeService = workTimeService;
        _projectService = projectService;
    }

    public async Task<IActionResult> Index()
    {
        var userId = Guid.Empty;

        var currentWorkTime = await _workTimeService.GetOpenWorkTimeAsync(userId);
        var projects = await _projectService.GetAllAsync();

        var model = new WorkTimeViewModel
        {
            OpenWorkTime = currentWorkTime,
            Projects = new SelectList(projects, "Id", "Title")
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> ClockIn(Guid employeeId, Guid projectId)
    {
        await _workTimeService.ClockInAsync(employeeId, projectId);

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> ClockOut(Guid employeeId)
    {
        await _workTimeService.ClockOutAsync(employeeId);

        return RedirectToAction("Index");
    }
}
