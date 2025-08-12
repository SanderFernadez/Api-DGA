using Api_DGA.Core.Entities;
using Api_DGA.Application.Dtos.Product;
using AutoMapper;

namespace Api_DGA.Application.Mappings
{
    /// <summary>
    /// Perfil de mapeo para la entidad Product
    /// </summary>
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            // Mapeo de CreateProductDto a Product
            CreateMap<CreateProductDto, Product>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.SaleProducts, opt => opt.Ignore());

            // Mapeo de UpdateProductDto a Product
            CreateMap<UpdateProductDto, Product>()
                .ForMember(dest => dest.SaleProducts, opt => opt.Ignore());

            // Mapeo de Product a GetProductDto
            CreateMap<Product, GetProductDto>();

            // Mapeo de Product a ProductSearchDto
            CreateMap<Product, ProductSearchDto>();

            // Mapeo de Product a TopSellingProductDto
            CreateMap<Product, TopSellingProductDto>();
        }
    }
}
