using GamblingGame.Models.DTO;
using GamblingGame.Repository;

namespace GamblingGame.Service
{
    public class GambleService : IGambleService
    {
        private IGambleRepository _repository;
        private GambleResult _result;
        private ILogger<GambleService> _logger;

        public GambleService(IGambleRepository repository, ILogger<GambleService> logger)
        {
            _repository = repository;
            _logger = logger;
            _result = new GambleResult();
        }

        /// <summary>
        /// Verify Username and Password
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public async Task<UsersDto> AuthenticateUser(Login loginInfo)
        {
            UsersDto user = await _repository.GetUserByName(loginInfo.UserName);
            if (user != null && loginInfo.Password == user.Password)
                return user;
            else
                return null;

        }

        /// <summary>
        /// Gamble game:
        /// Check if user has enough balance
        /// Compare the bet and lucky number
        /// Update user account with balance points
        /// </summary>
        /// <param name="input"></param>
        /// <param name="userId"></param>
        /// <param name="luckyNum"></param>
        /// <returns></returns>
        public async Task<GambleResult> Gamble(GambleInput input, string userId , int luckyNum)
        {
            try
            {
                UsersDto user = await _repository.GetUserByName(userId);
                //check if user has enough balance
                AccountsDto intialAccount = await _repository.GetAccount(user.Id);
                _logger.LogDebug(userId + " Current: " + intialAccount.Balance);
                bool hasBalance = CheckBalance(intialAccount, input.Points);
                if (hasBalance)
                {
                    intialAccount.Balance -= input.Points;
                    //update stake points before the game starts
                    AccountsDto userAccount = await _repository.UpdateAccount(intialAccount);
                    _logger.LogInformation("Lucky Number:" + luckyNum);
                    if (input.Bet == luckyNum)
                    {
                        _logger.LogInformation("Player won the Bet");
                        //get acccount for updating the win points
                        AccountsDto winAccount = await _repository.GetAccount(user.Id);
                        winAccount.Balance += Constants.Count * input.Points;
                        AccountsDto newBalance = await _repository.UpdateAccount(winAccount);
                        _logger.LogDebug("New balance:" + newBalance.Balance);
                        _result.AccountBalance = newBalance.Balance;
                        _result.Points = "+" + Constants.Count * input.Points;
                    }
                    else
                    {
                        _logger.LogInformation("Player lost the Bet");
                        _result.AccountBalance = userAccount.Balance;
                        _result.Status = "Lost";
                        _result.Points = "-" + input.Points;
                    }
                }
                
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool CheckBalance(AccountsDto account, int points)
        {
            if (account.Balance - points >= 0)
            {
                return true;
            }
            else
                throw new Exception("Player does not have enough balance!"); ;
        }

        /// <summary>
        /// To compare result with user account
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<AccountsDto> GetAccount(string userId)
        {
            UsersDto user = await _repository.GetUserByName(userId);
            return await _repository.GetAccount(user.Id);
        }

        /// <summary>
        /// Random lucky number generator
        /// </summary>
        /// <returns></returns>
        public int GenerateLuckyNumber()
        {
            Random r = new Random();
            int randomNumber = r.Next(0, 9);
            return randomNumber;
        }
    }
}
