using AutoMapper;
using CAMT.Domain.Entities.Catalog;

namespace CAMT.Application.Modules.Catalog.Products.Shared;

public class ProductProfile : Profile
{
	public ProductProfile()
	{
		CreateMap<Product, ProductDto>();
	}
}