namespace CAMT.Application.Modules.Catalog.Products.Create;

public record CreateProductCmd : IRequest<ErrorOr<Guid>>
{
    public string Name { get; set; }
    public decimal Price { get; set; }
}