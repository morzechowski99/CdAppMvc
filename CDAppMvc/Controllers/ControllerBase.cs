using CDAppMvc.Enums;
using Microsoft.AspNetCore.Mvc;

namespace CDAppMvc.Controllers;

public class ControllerBase : Controller
{
    internal IActionResult GetActionResult(ActionStatus status, string? viewName = null, object? model = null) =>
        status switch
        {
            ActionStatus.NotFound => NotFound(),
            ActionStatus.Unauthorized => Unauthorized(),
            _ => throw new ArgumentOutOfRangeException(nameof(status)),
        };
}