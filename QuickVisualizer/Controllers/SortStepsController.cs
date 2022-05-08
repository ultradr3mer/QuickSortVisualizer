
using Microsoft.AspNetCore.Mvc;
using QuickVisualizer.Models;

namespace QuickVisualizer.Controllers;

public class SortStepsController : Controller
{
    private readonly ILogger<SortStepsController> _logger;

    public SortStepsController(ILogger<SortStepsController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index()
    {
        int[] arr = new int[] { 2, 5, -4, 11, 0, 18, 22, 67, 51, 6 };

        var solution = QuickSortSolver.QuickSort(arr);

        return View(solution);
    }
}