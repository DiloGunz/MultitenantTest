using AutoMapper;
using CAMT.Application.Modules.Catalog.Products.Shared;
using CAMT.Domain.Entities.Catalog;
using CAMT.Domain.Entities.Catalog.Interfaces;

namespace CAMT.Application.Modules.Catalog.Products.GetAll;

public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, ErrorOr<IReadOnlyList<ProductDto>>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetAllProductsHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<ErrorOr<IReadOnlyList<ProductDto>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            IReadOnlyList<Product> products = await _productRepository.GetAllAsync();

            return _mapper.Map<List<ProductDto>>(products).OrderBy(x => x.Name).ToList();
        }
        catch (Exception ex)
        {
            return Error.Failure(ex.Message);
        }
                
    }
}