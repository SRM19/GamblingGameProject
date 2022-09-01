using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamblingGame.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }

        public int Balance { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}
