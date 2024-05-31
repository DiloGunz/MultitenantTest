
using CAMT.Domain.Entities.Catalog.Interfaces;

namespace CAMT.Application.Modules.Catalog.Products.Delete;

public class DeleteProductHandler : IRequestHandler<DeleteProductCmd, ErrorOr<Unit>>
{
    private readonly IProductRepository _productRepository;

    public DeleteProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<ErrorOr<Unit>> Handle(DeleteProductCmd request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);

        if (product == null)
        {
            return Error.NotFound("Product.NotFound", "The product with the provide Id was not found.");
        }

        await _productRepository.Delete(product);

        return Unit.Value;
    }
}