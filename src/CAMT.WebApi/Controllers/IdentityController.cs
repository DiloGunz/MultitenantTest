using CAMT.Application.Modules.Identity.Login;
using Microsoft.AspNetCore.Authorization;

namespace CAMT.WebApi.Controllers;

/// <summary>
/// Controller to manage login user
/// </summary>
[AllowAnonymous]
[Route("api/[controller]")]
public class IdentityController : ApiController
{
    private readonly ISender _mediator;

    public IdentityController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("authentication")]
    public async Task<IActionResult> Authentication([FromBody] LoginCmd command)
    {
        var result = await _mediator.Send(command);
        return result.Match(login => Ok(login), errors => Problem(errors));
    }
}