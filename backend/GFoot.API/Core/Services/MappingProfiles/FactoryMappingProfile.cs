
namespace Services
{
    internal class FactoryMappingProfile : Profile
    {
        public FactoryMappingProfile()
        {
            CreateMap<FactoryUser, FactoryUserDto>()
                .ReverseMap();


            CreateMap<FactoryEmission, FactoryEmissionDTO>().ReverseMap();
        }
    }
}
