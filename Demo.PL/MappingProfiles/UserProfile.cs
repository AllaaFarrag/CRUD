using AutoMapper;
using Demo.DAL.Entites;
using Demo.PL.ViewModels;

namespace Demo.PL.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<AppUser, UserVM>();
        }
    }
}
