using Microsoft.AspNetCore.Mvc.Rendering;
using VeronikaKorzhevykh.TimeTrackingTask.Core.Models;

namespace VeronikaKorzhevykh.TimeTrackingTask.ViewModels;

public class WorkTimeViewModel
{
    public WorkTime OpenWorkTime { get; set; }
    public SelectList Projects { get; set; }
}
