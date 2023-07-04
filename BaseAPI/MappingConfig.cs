using AutoMapper;
using BaseAPI.Data;
using BaseAPI.Models.Dto;

namespace BaseAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<ClasePrincipal, ClasePrincipalDto>();
            CreateMap<ClasePrincipalDto, ClasePrincipal>();

            CreateMap<ClasePrincipal, ClasePrincipalCreateDto>().ReverseMap();
            CreateMap<ClasePrincipal, ClasePrincipalUpdateDto>().ReverseMap();

            CreateMap<Class1, Class1Dto>();
            CreateMap<Class1Dto, Class1>();

            CreateMap<Class1, Class1CreateDto>().ReverseMap();
            CreateMap<Class1, Class1UpdateDto>().ReverseMap();

            CreateMap<Class2, Class2Dto>();
            CreateMap<Class2Dto, Class2>();

            CreateMap<Class2, Class2CreateDto>().ReverseMap();
            CreateMap<Class2, Class2UpdateDto>().ReverseMap();
        }
    }
}
