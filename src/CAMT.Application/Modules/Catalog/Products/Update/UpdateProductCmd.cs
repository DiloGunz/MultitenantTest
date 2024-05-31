namespace CAMT.Application.Modules.Catalog.Products.Update;

public record UpdateProductCmd : IRequest<ErrorOr<Unit>>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}