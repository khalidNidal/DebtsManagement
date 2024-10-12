using AutoMapper;
using DebtsManagement.Core.Entities.DTO.CustomerDTO;
using DebtsMangment.Core.Entities;

namespace DebtsManagement.API.mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDTOResponse>();
            CreateMap<Customer, CustomerDTORequest>().ReverseMap();
        }
    }
}
