using CAMT.Application.Modules.Catalog.Products.Shared;

namespace CAMT.Application.Modules.Catalog.Products.GetAll;

public record GetAllProductsQuery : IRequest<ErrorOr<IReadOnlyList<ProductDto>>>
{

}
