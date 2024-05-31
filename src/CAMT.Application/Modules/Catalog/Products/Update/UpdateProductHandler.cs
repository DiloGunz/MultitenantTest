
using CAMT.Domain.Entities.Catalog.Interfaces;

namespace CAMT.Application.Modules.Catalog.Products.Update;

public class UpdateProductHandler : IRequestHandler<UpdateProductCmd, ErrorOr<Unit>>
{
    private readonly IProductRepository _productRepository;

    public UpdateProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<ErrorOr<Unit>> Handle(UpdateProductCmd request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);

        if (product == null)
        {
            return Error.NotFound("Product.NotFound", "The product with the provide Id was not found.");
        }

        product.Name = request.Name.ToUpper().Trim();
        product.Price = request.Price;

        await _productRepository.Update(product);

        return Unit.Value;
    }
}