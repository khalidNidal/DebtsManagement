using AutoMapper;
using DebtsManagement.Core.Entities.DTO.CustomerDTO;
using DebtsManagement.Core.Entities.DTO.DebtsDTO;
using DebtsMangment.Core.Entities;

namespace DebtsManagement.API.mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Debts , DebtsDTOResponse>().
                ForMember(to => to.CustomerName, from=>from.MapFrom(x=>x.Customer != null ? x.Customer.CustomerName : null));
            CreateMap<Debts, DebtsDTORequest>().
                ForMember(to => to.CustomerId, from => from.MapFrom(x => x.Customer == null ? x.Customer.Id : 0)).ReverseMap();
            CreateMap<Customer, CustomerDTOResponse>();
            CreateMap<Customer, CustomerDTORequest>().ReverseMap();
        }
    }
}
