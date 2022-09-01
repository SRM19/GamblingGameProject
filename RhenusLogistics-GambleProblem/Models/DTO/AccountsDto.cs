namespace GamblingGame.Models.DTO;
public class AccountsDto
{
    public int Id { get; set; }
    public int UserId { get; set; }

    public int Balance { get; set; }

    public User User { get; set; }
}
