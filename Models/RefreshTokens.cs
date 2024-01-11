using System.ComponentModel.DataAnnotations;

namespace PokeApi.Models
{
    public class RefreshTokens : BaseModel
    {
        [Required]
        public string TrainerId { get; set; }

        public Trainer Trainer { get; set; }

        [Required]
        public string RefreshToken { get; set; }

        public bool? IsActive { get; set; } = true;

        public RefreshTokens() : base() {
        }
    }
}
