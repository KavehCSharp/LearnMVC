using AutoMapper;
using CrudImageRepository.Models;

namespace CrudImageRepository
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserViewModel>();
            CreateMap<UserViewModel, User>();
        }
    }
}