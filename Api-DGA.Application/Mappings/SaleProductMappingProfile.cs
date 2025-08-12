using Api_DGA.Core.Entities;
using Api_DGA.Application.Dtos.SaleProduct;
using AutoMapper;

namespace Api_DGA.Application.Mappings
{
    /// <summary>
    /// Perfil de mapeo para la entidad SaleProduct
    /// </summary>
    public class SaleProductMappingProfile : Profile
    {
        public SaleProductMappingProfile()
        {
            // Mapeo de CreateSaleProductDto a SaleProduct
            CreateMap<CreateSaleProductDto, SaleProduct>()
                .ForMember(dest => dest.Sale, opt => opt.Ignore())
                .ForMember(dest => dest.Product, opt => opt.Ignore());

            // Mapeo de UpdateSaleProductDto a SaleProduct
            CreateMap<UpdateSaleProductDto, SaleProduct>()
                .ForMember(dest => dest.Sale, opt => opt.Ignore())
                .ForMember(dest => dest.Product, opt => opt.Ignore());

            // Mapeo de SaleProduct a GetSaleProductDto
            CreateMap<SaleProduct, GetSaleProductDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product != null ? src.Product.Name : string.Empty))
                .ForMember(dest => dest.SaleDate, opt => opt.MapFrom(src => src.Sale != null ? src.Sale.Date : DateTime.MinValue));
        }
    }
}
