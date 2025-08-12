using Api_DGA.Core.Entities;
using Api_DGA.Application.Dtos.Sale;
using AutoMapper;

namespace Api_DGA.Application.Mappings
{
    /// <summary>
    /// Perfil de mapeo para la entidad Sale
    /// </summary>
    public class SaleMappingProfile : Profile
    {
        public SaleMappingProfile()
        {
            // Mapeo de CreateSaleDto a Sale
            CreateMap<CreateSaleDto, Sale>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.SaleProducts, opt => opt.Ignore())
                .ForMember(dest => dest.Client, opt => opt.Ignore());

            // Mapeo de UpdateSaleDto a Sale
            CreateMap<UpdateSaleDto, Sale>()
                .ForMember(dest => dest.SaleProducts, opt => opt.Ignore())
                .ForMember(dest => dest.Client, opt => opt.Ignore());

            // Mapeo de Sale a GetSaleDto
            CreateMap<Sale, GetSaleDto>()
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client != null ? src.Client.Email : string.Empty));

            // Mapeo de Sale a GetSaleDetailDto
            CreateMap<Sale, GetSaleDetailDto>()
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client != null ? src.Client.Email : string.Empty))
                .ForMember(dest => dest.SaleProducts, opt => opt.MapFrom(src => src.SaleProducts));

            // Mapeo de Sale a SaleReportDto
            CreateMap<Sale, SaleReportDto>()
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client != null ? src.Client.Email : string.Empty));
        }
    }
}
