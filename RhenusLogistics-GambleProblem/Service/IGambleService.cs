using GamblingGame.Models.DTO;

namespace GamblingGame.Service
{
    public interface IGambleService
    {
        Task<UsersDto> AuthenticateUser(Login loginInfo);

        Task<GambleResult> Gamble(GambleInput input,string userId, int luckyNum);

        int GenerateLuckyNumber();

        Task<AccountsDto> GetAccount(string userId);
    }
}
