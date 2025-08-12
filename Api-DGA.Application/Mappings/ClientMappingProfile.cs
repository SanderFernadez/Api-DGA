using Api_DGA.Core.Entities;
using Api_DGA.Application.Dtos.Client;
using AutoMapper;

namespace Api_DGA.Application.Mappings
{
    /// <summary>
    /// Perfil de mapeo para la entidad Client
    /// </summary>
    public class ClientMappingProfile : Profile
    {
        public ClientMappingProfile()
        {
            // Mapeo de CreateClientDto a Client
            CreateMap<CreateClientDto, Client>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Sales, opt => opt.Ignore());

            // Mapeo de UpdateClientDto a Client
            CreateMap<UpdateClientDto, Client>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Sales, opt => opt.Ignore());

            // Mapeo de Client a GetClientDto
            CreateMap<Client, GetClientDto>();

            // Mapeo de Client a ClientSearchDto
            CreateMap<Client, ClientSearchDto>();
        }
    }
}
