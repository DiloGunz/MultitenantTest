namespace CAMT.Application.Modules.Catalog.Products.Delete;

public record DeleteProductCmd(Guid Id) : IRequest<ErrorOr<Unit>>;