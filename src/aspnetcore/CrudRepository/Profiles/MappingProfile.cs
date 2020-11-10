using AutoMapper;
using CrudRepository.Models;

namespace CrudRepository.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerViewModel>();
            CreateMap<CustomerViewModel, Customer>();
        }
    }
}