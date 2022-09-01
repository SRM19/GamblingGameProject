using System.ComponentModel.DataAnnotations;

namespace GamblingGame.Models.DTO
{
    public class GambleResult
    {
        [Display(Name = "Account Balance")]
        public int AccountBalance { get; set; }
        public string Status { get; set; } = "Won";
        public string Points { get; set; }
    }
}
