using FootballLeagueApp.Common.Common.Models;
using FootballLeagueApp.Common.Helpers;
using FootballLeagueApp.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeagueApp.Common.Validation
{
    public class ValidationFilter : IAsyncActionFilter
    {
        private readonly ILogger<ValidationFilter> _logger;

        private readonly IDefaultErrorCodeProvider _defaultErrorCodeProvider;

        public ValidationFilter(ILogger<ValidationFilter> logger, IDefaultErrorCodeProvider defaultErrorCodeProvider)
        {
            _logger = logger;
            _defaultErrorCodeProvider = defaultErrorCodeProvider;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var response = CreateResponseModel(context);

                context.Result = new BadRequestObjectResult(response);

                return;
            }

            await next();
        }

        public ErrorResponse CreateResponseModel(ActionExecutingContext context)
        {
            var response = new ErrorResponse();

            var messages = context.ModelState.Values.Where(E => E.Errors.Count > 0)
                .SelectMany(E => E.Errors)
                .Select(E => E.ErrorMessage)
                .ToList();

            response.Message = string.Join(Environment.NewLine, messages);
            response.ErrorCode = _defaultErrorCodeProvider.DefaultValidationErrorCode;

            LogErrors(context, response);
            return response;
        }

        private void LogErrors(ActionExecutingContext context, ErrorResponse response)
        {
            _logger.LogError("RequestId: {requestHeaders}; ErrorCode: {errorCode}; Message: {responseMessage}",
                context.HttpContext.Request.Headers.FirstOrDefault(x => x.Key == HeadersHelper.XInputRequestId),
                response.ErrorCode,
                string.Join(Environment.NewLine, response.Message));
        }
    }
}
