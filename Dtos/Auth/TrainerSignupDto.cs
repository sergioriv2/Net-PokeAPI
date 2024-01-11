using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PokeApi.Dtos.Auth
{
    public class TrainerSignupDto
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [EmailAddress]
        [Required]
        public string Username { get; set; }

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{8,}$")]
        public string Password { get; set; }
    }
}
