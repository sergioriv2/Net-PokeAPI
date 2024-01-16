namespace PokeApi.Filters
{
    public class CustomValidationException : Exception
    {
        public CustomValidationCodes ExceptionCode { get; set; }
        public string Message { get; set; }

        public CustomValidationException(CustomValidationCodes exceptionCode) : base()
        {
            Message = CustomValidationExceptionsDictionary.Messages[CustomValidationCodes.EmailAlreadyOnUse];
            ExceptionCode = exceptionCode;
        }

        //public CustomValidationException(CustomValidationCodes exceptionCode, object? args) : base()
        //{
        //    Message = CustomValidationExceptionsDictionary.Messages[CustomValidationCodes.EmailAlreadyOnUse];
        //    ExceptionCode = exceptionCode;
        //}
    }

    public enum CustomValidationCodes
    {
        // Auth -- 100x
        EmailAlreadyOnUse = 1001,
        PasswordsDoesntMatch = 1002,
        InvalidTrainerEmail = 1003,
        // Trainer -- 200x
        TrainerNotFound = 2001,
    }

    public static class CustomValidationExceptionsDictionary
    {
        public static readonly Dictionary<CustomValidationCodes, string> Messages = new Dictionary<CustomValidationCodes, string>()
        {
            // Email Validations
            {
                CustomValidationCodes.EmailAlreadyOnUse, "Email already on use."
            },
            {
                CustomValidationCodes.PasswordsDoesntMatch, "The credentials are invalid."
            },
                 {
                CustomValidationCodes.InvalidTrainerEmail, "The credentials are invalid."
            },
            // Trainer Validations -- 200x
            {
                CustomValidationCodes.TrainerNotFound, "Trainer not found."
            }
        };
    }
}
