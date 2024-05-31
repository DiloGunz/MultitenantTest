using AutoMapper;
using CAMT.Application.Modules.Catalog.Products.Shared;
using CAMT.Domain.Entities.Catalog.Interfaces;

namespace CAMT.Application.Modules.Catalog.Products.GetById;

public class GetByIdProductHandler : IRequestHandler<GetByIdProductQuery, ErrorOr<ProductDto>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetByIdProductHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }


    public async Task<ErrorOr<ProductDto>> Handle(GetByIdProductQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);

        if (product == null)
        {
            return Error.NotFound("Product.NotFound", "The product with the provide Id was not found.");
        }

        return _mapper.Map<ProductDto>(product);
    }
}