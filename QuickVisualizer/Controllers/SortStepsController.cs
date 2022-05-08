
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QuickVisualizer.Data;
using QuickVisualizer.Models;

namespace QuickVisualizer.Controllers;

[Route("[controller]")]
public class SortStepsController : Controller
{
    private readonly ILogger<SortStepsController> _logger;

    public SortStepsController(ILogger<SortStepsController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var arr = new int[] { 2, 5, -4, 11, 0, 18, 22, 67, 51, 6 };
        var values = string.Join(", ", arr);

        var solution = QuickSortSolver.QuickSort(arr, showSwitches: true);

        return this.View(nameof(Index), new SortingStepsData() 
        { 
            Values = values, 
            Solution = solution,
            ShowSwitches = true
        });
    }

    [HttpPost]
    public IActionResult Post(SortingStepsData data)
    {
        try
        {
            var arr = data.Values.Split(",").Select(v => int.Parse(v)).ToArray();

            data.Solution = QuickSortSolver.QuickSort(arr, data.ShowSwitches);

            return this.View(nameof(Index), data);
        }
        catch
        {
            return this.View(nameof(Index), data);
        }
    }

    [HttpGet("stepimg/{arrayJson}")]
    public IActionResult RenderStep(string arrayJson)
    {
        var step = JsonConvert.DeserializeObject<QuickSortSolutionStep>(arrayJson);

        var bytes = QuickSortSolutionRenderer.Render(step);

        return File(bytes, "image/png");
    }
}