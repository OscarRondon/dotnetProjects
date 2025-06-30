using AutoMapper;
using Shortly.Data.Models;
using Shortly.Client.Data.ViewMoels;

namespace Shortly.Client.Data.Mapper
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Url, GetUrlVM>().ReverseMap();
            CreateMap<AppUser, GetUserVM>().ReverseMap();
        }
    }
}
