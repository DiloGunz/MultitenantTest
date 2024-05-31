using CAMT.Application.Modules.Company.Organizations.Create;
using CAMT.Application.Modules.Company.Organizations.Delete;
using CAMT.Application.Modules.Company.Organizations.GetAll;
using CAMT.Application.Modules.Company.Organizations.GetById;
using CAMT.Application.Modules.Company.Organizations.Update;

namespace CAMT.WebApi.Controllers;

/// <summary>
/// Controller to manage organizations
/// </summary>
[Route("api/[controller]")]
public class OrganizationController : ApiController
{
    private readonly ISender _mediator;

    public OrganizationController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var getAllResult = await _mediator.Send(new GetAllOrganizationQuery());
        return getAllResult.Match(Ok, Problem);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var getAllResult = await _mediator.Send(new GetByIdOrganizationQuery(id));
        return getAllResult.Match(create => Ok(create), errors => Problem(errors));
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrganizationCmd command)
    {
        var getAllResult = await _mediator.Send(command);
        return getAllResult.Match(create => Ok(create), errors => Problem(errors));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateOrganizationCmd command)
    {
        if (command.Id != id)
        {
            List<Error> errors = new()
            {
                Error.Validation("Organization.UpdateInvalid", "The request Id does not match with the url Id.")
            };
            return Problem(errors);
        }
        var getAllResult = await _mediator.Send(command);
        return getAllResult.Match(create => Ok(create), errors => Problem(errors));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var getAllResult = await _mediator.Send(new DeleteOrganizationCmd(id));
        return getAllResult.Match(create => Ok(create), errors => Problem(errors));
    }

}