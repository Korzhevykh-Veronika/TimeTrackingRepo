using Microsoft.AspNetCore.Mvc;
using VeronikaKorzhevykh.TimeTrackingTask.Application.Services.Interfaces;

namespace VeronikaKorzhevykh.TimeTrackingTask.Controllers;

public class ReportController : Controller
{
    private readonly IWorkTimeService _workTimeService;

    public ReportController(IWorkTimeService workTimeService)
    {
        _workTimeService = workTimeService;
    }

    public async Task<IActionResult> Index(string searchString)
    {
        ViewData["CurrentFilter"] = searchString;
        var userId = Guid.Empty;

        var allWorkItems = await _workTimeService.GetWorkHistoryAsync(userId);

        if (!string.IsNullOrEmpty(searchString))
        {
            allWorkItems = allWorkItems.Where(w => w.Project.Title.Contains(searchString)).ToList();
        }

        return View(allWorkItems);
    }
}
