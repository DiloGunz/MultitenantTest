using Microsoft.AspNetCore.Diagnostics;

namespace CAMT.WebApi.Controllers;

/// <summary>
/// Controller to manage errors
/// </summary>
public class ErrorsControler : ControllerBase
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("/error")]
    public IActionResult Error()
    {
        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        return Problem();
    }
}