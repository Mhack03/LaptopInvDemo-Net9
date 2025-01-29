using API.Models.Dto;
using API.Models;
using AutoMapper;

namespace API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Assignment, AssignmentDto>().ReverseMap()
                .ForMember(dest => dest.ReturnDate, opt => opt
                .MapFrom(src => src.ReturnDate));
            CreateMap<Laptop, LaptopDto>().ReverseMap();
            CreateMap<Employee, EmployeeDto>().ReverseMap();

            CreateMap<AssignmentCreateDto, Assignment>();
            CreateMap<AssignmentUpdateDto, Assignment>();
            CreateMap<LaptopCreateDto, Laptop>();
            CreateMap<LaptopUpdateDto, Laptop>();
            CreateMap<EmployeeCreateDto, Employee>();
            CreateMap < EmployeeUpdateDto, Employee>();
        }
    }
}
