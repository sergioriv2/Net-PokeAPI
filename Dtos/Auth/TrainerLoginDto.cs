using FluentValidation;

namespace PokeApi.Dtos.Auth
{
    public class TrainerLoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class TrainerLoginValidator : AbstractValidator<TrainerLoginDto>
    {
        public TrainerLoginValidator()
        {
            RuleFor(x => x.Username).EmailAddress().NotEmpty().NotNull();
            RuleFor(x => x.Password).Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{8,}$").NotNull().NotEmpty();
        }
    }

    public class TrainerLoginResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
