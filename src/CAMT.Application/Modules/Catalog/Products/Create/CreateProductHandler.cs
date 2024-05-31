using CAMT.Domain.Entities.Catalog;
using CAMT.Domain.Entities.Catalog.Interfaces;

namespace CAMT.Application.Modules.Catalog.Products.Create;

public class CreateProductHandler : IRequestHandler<CreateProductCmd, ErrorOr<Guid>>
{
    private readonly IProductRepository _productRepository;

    public CreateProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<ErrorOr<Guid>> Handle(CreateProductCmd request, CancellationToken cancellationToken)
    {
        var product = new Product()
        {
            Name = request.Name.ToUpper().Trim(),
            Price = request.Price
        };

        await _productRepository.AddAsync(product);

        return product.Id;
    }
}