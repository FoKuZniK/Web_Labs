using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Company, CompanyDto>().ForMember(c => c.FullAddress, opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));
        CreateMap<Employee, EmployeeDto>();
        CreateMap<Order, OrderDto>();
        CreateMap<Warehouse, WarehouseDto>();

        CreateMap<CompanyForCreationDto, Company>();
        CreateMap<EmployeeForCreationDto, Employee>();
        CreateMap<WarehouseForCreationDto, Warehouse>();
        CreateMap<OrderForCreationDto, Order>();

        CreateMap<EmployeeForUpdateDto, Employee>();
        CreateMap<CompanyForUpdateDto, Company>();
        CreateMap<EmployeeForUpdateDto, Employee>().ReverseMap();
        CreateMap<OrderForUpdateDto, Order>();
        CreateMap<WarehouseForUpdateDto, Warehouse>();
        CreateMap<OrderForUpdateDto, Order>().ReverseMap();
    }
}
