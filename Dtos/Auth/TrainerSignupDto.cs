using FluentValidation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PokeApi.Dtos.Auth
{
    public class TrainerSignupDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class TrainerSignupValidation : AbstractValidator<TrainerSignupDto>
    {
        public TrainerSignupValidation()
        {
            RuleFor(x => x.FirstName).NotEmpty().NotNull().MaximumLength(50);
            RuleFor(x => x.LastName).NotEmpty().NotNull().MaximumLength(50);
            RuleFor(x => x.Username).EmailAddress();
            RuleFor(x => x.Password).Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{8,}$");
        }
    }
}
