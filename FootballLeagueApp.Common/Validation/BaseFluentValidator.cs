using FluentValidation;
using FluentValidation.Results;
using FootballLeagueApp.Common.Exceptions.Models;
using FootballLeagueApp.Common.Interfaces;
using System.Net;

namespace FootballLeagueApp.Common.Validation
{
    public abstract class BaseFluentValidator<T> : AbstractValidator<T> where T : class
    {
        private readonly IDefaultErrorCodeProvider defaultErrorCodeProvider;

        public BaseFluentValidator(IDefaultErrorCodeProvider defaultErrorCodeProvider)
        {
            this.defaultErrorCodeProvider = defaultErrorCodeProvider;
        }

        protected override void RaiseValidationException(ValidationContext<T> context, ValidationResult result)
        {
            string errorCode = defaultErrorCodeProvider.DefaultValidationErrorCode;
            string errorMessage = result.Errors[0].ErrorMessage;

            if (!string.IsNullOrEmpty(result.Errors[0].ErrorCode) && result.Errors[0].ErrorCode.StartsWith("ER"))
            {
                errorCode = result.Errors[0].ErrorCode;
            }

            throw new GeneralException(nameof(T), HttpStatusCode.BadRequest, errorCode, errorMessage, ExceptionType.WARNING);
        }
    }
}
