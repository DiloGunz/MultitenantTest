using CAMT.Application.Modules.Company.AppUsers.Create;
using CAMT.Application.Modules.Company.AppUsers.GetAll;

namespace CAMT.WebApi.Controllers;

/// <summary>
/// Controller to manage users
/// </summary>
[Route("api/[controller]")]
public class AppUserController : ApiController
{
    private readonly ISender _mediator;

    public AppUserController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var getAllResult = await _mediator.Send(new GetAllAppUserQuery());
        return getAllResult.Match(Ok, Problem);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAppUserCmd command)
    {
        var getAllResult = await _mediator.Send(command);
        return getAllResult.Match(create => Ok(create), errors => Problem(errors));
    }
}