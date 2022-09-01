using AutoMapper;
using GamblingGame.Models;
using GamblingGame.Models.DTO;

namespace GamblingGame
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<UsersDto, User>();
                config.CreateMap<User, UsersDto>();
                config.CreateMap<AccountsDto, Account>();
                config.CreateMap<Account, AccountsDto>();
            });
            return mappingConfig;
        }
    }
}
