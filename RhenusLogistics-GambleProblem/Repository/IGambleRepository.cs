using GamblingGame.Models.DTO;

namespace GamblingGame.Repository
{
    public interface IGambleRepository
    {
        Task<UsersDto> GetUserById(int userId);

        Task<UsersDto> GetUserByName(string name);

        Task<AccountsDto> GetAccount(int userId);

        Task<AccountsDto> UpdateAccount(AccountsDto accountsDto);

    }
}
