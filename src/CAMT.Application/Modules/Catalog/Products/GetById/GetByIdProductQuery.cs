using CAMT.Application.Modules.Catalog.Products.Shared;

namespace CAMT.Application.Modules.Catalog.Products.GetById;

public record GetByIdProductQuery(Guid Id) : IRequest<ErrorOr<ProductDto>>;