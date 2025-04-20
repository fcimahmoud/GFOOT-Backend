using AutoMapper;

namespace Services
{
    internal class FactoryMappingProfile : Profile
    {
        public FactoryMappingProfile()
        {
            CreateMap<FactoryUser, FactoryProfileDTO>()
                .ForMember(D => D.DisplayName, O => O.MapFrom(src => src.ApplicationUser!.DisplayName))
                .ForMember(D => D.Country, O => O.MapFrom(src => src.ApplicationUser!.Country))
                .ForMember(D => D.City, O => O.MapFrom(src => src.ApplicationUser!.City))
                .ReverseMap();


            CreateMap<FactoryEmission, FactoryEmissionDTO>().ReverseMap();
        }
    }
}
