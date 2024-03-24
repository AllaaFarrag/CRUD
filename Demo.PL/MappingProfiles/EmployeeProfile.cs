using AutoMapper;
using Demo.DAL.Entites;
using Demo.PL.ViewModels;

namespace Demo.PL.MappingProfiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeVM, Employee>().ReverseMap();
        }
    }
}
