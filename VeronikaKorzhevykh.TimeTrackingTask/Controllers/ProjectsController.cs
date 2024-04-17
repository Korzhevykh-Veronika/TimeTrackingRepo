using Microsoft.AspNetCore.Mvc;
using VeronikaKorzhevykh.TimeTrackingTask.Application.Services.Interfaces;
using VeronikaKorzhevykh.TimeTrackingTask.Core.Models;

namespace VeronikaKorzhevykh.TimeTrackingTask.Controllers;

public class ProjectsController : Controller
{
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    public async Task<IActionResult> Index()
    {
        var projects = await _projectService.GetAllAsync();

        return View(projects); 
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Title,Description")] Project project)
    {
        if (ModelState.IsValid)
        {
            await _projectService.CreateAsync(project);

            return RedirectToAction(nameof(Index));
        }

        return View(project);
    }

    public async Task<IActionResult> Edit(Guid? id)
    {
        var project = await _projectService.GetAsync(id ?? Guid.Empty);

        if (project == null)
        {
            return NotFound();
        }

        return View(project);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,Description")] Project project)
    {
        if (id != project.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            await _projectService.UpdateAsync(project);

            return RedirectToAction(nameof(Index));
        }

        return View(project);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _projectService.DeleteAsync(id);

        return RedirectToAction(nameof(Index));
    }
}
