using AutoMapper;
using Demo.DAL.Entites;
using Demo.PL.ViewModels;

namespace Demo.PL.MappingProfiles
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<DepartmentVM , Department>().ReverseMap();
        }
    }
}
