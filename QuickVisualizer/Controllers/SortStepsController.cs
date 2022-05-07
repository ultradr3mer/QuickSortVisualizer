
using Microsoft.AspNetCore.Mvc;

namespace QuickVisualizer.Controllers;

[ApiController]
[Route("Api/[controller]")]
public class SortStepsController : Controller
{
    private readonly ILogger<SortStepsController> _logger;

    public SortStepsController(ILogger<SortStepsController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult SplitSteps(int[] array)
    {
        return Ok("bnis");
    }
}