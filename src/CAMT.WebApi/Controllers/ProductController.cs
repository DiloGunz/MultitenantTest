using CAMT.Application.Modules.Catalog.Products.Create;
using CAMT.Application.Modules.Catalog.Products.Delete;
using CAMT.Application.Modules.Catalog.Products.GetAll;
using CAMT.Application.Modules.Catalog.Products.GetById;
using CAMT.Application.Modules.Catalog.Products.Update;

namespace CAMT.WebApi.Controllers;

/// <summary>
/// Controller to manage products
/// Receives a 'slugTenant' parameter in the route that will identify the user's tenant
/// </summary>
[Route("{slugTenant}/api/[controller]")]
public class ProductController : ApiController
{
    private readonly ISender _mediator;

    public ProductController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var productsResult = await _mediator.Send(new GetAllProductsQuery());
        return productsResult.Match(
            products => Ok(products), 
            errors => Problem(errors));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var getAllResult = await _mediator.Send(new GetByIdProductQuery(id));
        return getAllResult.Match(create => Ok(create), errors => Problem(errors));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductCmd command)
    {
        var createResult = await _mediator.Send(command);
        return createResult.Match(productId => Ok(productId), errors => Problem(errors));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductCmd command)
    {
        if (command.Id != id)
        {
            List<Error> errors = new()
            {
                Error.Validation("Product.UpdateInvalid", "The request Id does not match with the url Id.")
            };
            return Problem(errors);
        }

        var getAllResult = await _mediator.Send(command);
        return getAllResult.Match(create => Ok(create), errors => Problem(errors));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var getAllResult = await _mediator.Send(new DeleteProductCmd(id));
        return getAllResult.Match(create => Ok(create), errors => Problem(errors));
    }
}