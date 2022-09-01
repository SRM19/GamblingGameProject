using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GamblingGame.Models.DTO
{
    public class GambleInput
    {
        [Range(1, int.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        [DefaultValue(100)]
        public int Points { get; set; }

        [Range(0, 9, ErrorMessage = "The field {0} must be between 0-9.")]
        [DefaultValue(0)]
        public int Bet { get; set; } 

    }
}
