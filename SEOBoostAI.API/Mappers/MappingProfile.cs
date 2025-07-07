using AutoMapper;

namespace SEOBoostAI.API.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SEOBoostAI.Repository.Models.Element, SEOBoostAI.API.VIewModels.Requests.Element>().ReverseMap();
        }
    }
}
