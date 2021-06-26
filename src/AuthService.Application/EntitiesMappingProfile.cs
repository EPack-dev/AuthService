using AuthService.Application.Entities;
using AuthService.Model;
using AutoMapper;

namespace AuthService.Application
{
    public class EntitiesMappingProfile : Profile
    {
        public EntitiesMappingProfile()
        {
            CreateMap<Account, AccountEntity>()
                .ReverseMap();
        }
    }
}
