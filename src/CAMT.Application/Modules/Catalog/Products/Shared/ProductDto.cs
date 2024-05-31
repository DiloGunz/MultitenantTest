namespace CAMT.Application.Modules.Catalog.Products.Shared;

public record ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}