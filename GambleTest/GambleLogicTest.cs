using GamblingGame.Models.DTO;
using System.Threading.Tasks;
using Xunit;

namespace Gamble.UnitTests;
public class GambleLogicTest: Helper
{
    [Fact]
    public async Task Login_User_Success()
    {
        //Arrange
        Login loginInfo = new Login() { UserName = "Player2", Password = "Password2" };
        //Action
        var user = await _gambleService.AuthenticateUser(loginInfo);
        //Assert
        Assert.NotNull(user);
    }

    [Fact]
    public async Task Login_User_Failure()
    {
        //Arrange
        Login loginInfo = new Login() { UserName = "Player2", Password = "Password1" };
        //Action
        var user = await _gambleService.AuthenticateUser(loginInfo);
        //Assert
        Assert.Null(user);
    }

    [Fact]
    public async Task Player_Won_The_Bet()
    {

        //Arrange
        int luckyNum = 1;
        string player = "Player1";
        GambleInput input = new GambleInput() { Bet = 1, Points = 100 };
        //Action
        var gambleResult = await _gambleService.Gamble(input, player, luckyNum);
        //Assert
        Assert.Equal("Won", gambleResult.Status);
    }

    [Fact]
    public async Task Player_Lost_The_Bet()
    {
        //Arrange
        int luckyNum = 2;
        string player = "Player1";
        GambleInput input = new GambleInput() { Bet = 1, Points = 100 };
        //Action
        var gambleResult = await _gambleService.Gamble(input, player, luckyNum);
        //Assert
        Assert.Equal("Lost",gambleResult.Status);
    }

    [Fact]
    public async Task Player_Won_The_Bet_AccBalance_Points_Increase9x()
    {
        //Arrange
        int luckyNum = 3;
        string player = "Player1";       
        AccountsDto userAccount = await _gambleService.GetAccount(player);
        int initialBalance = userAccount.Balance;
        GambleInput input = new GambleInput() { Bet = 3, Points = 100 };
        int expectedBalance = 9 * input.Points + initialBalance - input.Points;
        string expectedPoints = "+" + 9 * input.Points;
        //Action
        var gambleResult = await _gambleService.Gamble(input, player, luckyNum);
        //Assert
        Assert.Equal(expectedBalance, gambleResult.AccountBalance);
        Assert.Equal(expectedPoints, gambleResult.Points);
    }

    [Fact]
    public async Task Player_Lost_The_Bet_AccBalance_Points_LoseStake()
    {
        //Arrange
        int luckyNum = 4;
        string player = "Player1";
        AccountsDto userAccount = await _gambleService.GetAccount(player);
        int initialBalance = userAccount.Balance;
        GambleInput input = new GambleInput() { Bet = 6, Points = 100 };
        int expectedBalance = initialBalance - input.Points;
        string expectedPoints = "-" + input.Points;
        //Action
        var gambleResult = await _gambleService.Gamble(input, player, luckyNum);
        //Assert
        Assert.Equal(expectedBalance, gambleResult.AccountBalance);
        Assert.Equal(expectedPoints, gambleResult.Points);
    }

    [Fact]
    public async Task Player_Account_Insufficient_Balance()
    {
        //Arrange
        int luckyNum = 7;
        string player = "Player1";
        AccountsDto userAccount = await _gambleService.GetAccount(player);
        int initialBalance = userAccount.Balance;
        GambleInput input = new GambleInput() { Bet = 1, Points = initialBalance+1 };
        //Action
        var ex = Assert.ThrowsAsync<System.Exception>(async () => await _gambleService.Gamble(input, player, luckyNum));
        //Assert
        Assert.Contains("Player does not have enough balance!", ex.Result.Message);
    }

}
