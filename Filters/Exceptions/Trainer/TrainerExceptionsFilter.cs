using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using PokeApi.Responses;
using System.Net;

namespace PokeApi.Filters.Exceptions.Trainer
{
    public class TrainerExceptionsFilter : IAsyncExceptionFilter
    {
        Task IAsyncExceptionFilter.OnExceptionAsync(ExceptionContext context)
        {
            int httpStatusCode = (int)HttpStatusCode.InternalServerError;
            string httpStatusDescription = Status.fromInt(httpStatusCode).Description;
            var errorsResponse = new List<APIResponseError>();

            if (context.Exception is CustomValidationException validationEx)
            {
                switch (validationEx.ExceptionCode)
                {
                    case CustomValidationCodes.EmailAlreadyOnUse:
                        httpStatusCode = (int)HttpStatusCode.BadRequest;
                        httpStatusDescription = Status.fromInt(httpStatusCode).Description;
                        errorsResponse.Add(
                                new APIResponseError()
                                {
                                    Property = "email",
                                    Constraints = new
                                    {
                                        EmailAlradyOnUse = CustomValidationExceptionsDictionary.Messages[validationEx.ExceptionCode]
                                    }
                                }
                            );
                        break;
                    case CustomValidationCodes.PasswordsDoesntMatch:
                    case CustomValidationCodes.InvalidTrainerEmail:
                        httpStatusCode = (int)HttpStatusCode.BadRequest;
                        httpStatusDescription = Status.fromInt(httpStatusCode).Description;
                        errorsResponse.Add(
                                new APIResponseError()
                                {
                                    Property = "username | password",
                                    Constraints = new
                                    {
                                        InvalidCredentials = CustomValidationExceptionsDictionary.Messages[validationEx.ExceptionCode]
                                    }
                                }
                            );
                        break;
                    default:
                        httpStatusCode = (int)HttpStatusCode.InternalServerError;
                        httpStatusDescription = Status.fromInt(httpStatusCode).Description;
                        errorsResponse.Add(new APIResponseError()
                        {
                            Property = "Unknown",
                            Constraints = new
                            {
                                UnhandledError = validationEx.ToString(),
                                validationEx.StackTrace
                            }
                        });
                        break;
                }
            }

            var APIErrorResponse = new APIResponse(
                httpStatusCode,
                httpStatusDescription
            )
            {
                Errors = errorsResponse,
                Payload = new object()
            };

            context.ExceptionHandled = true;
            context.Result = new ObjectResult(APIErrorResponse)
            {
                StatusCode = APIErrorResponse.StatusCode
            };
            return Task.CompletedTask;
        }
    }
}
