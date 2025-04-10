using FootballLeagueApp.Common.Common.Models;
using FootballLeagueApp.Common.Exceptions.Models;
using FootballLeagueApp.Common.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace FootballLeagueApp.Common.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<ExceptionMiddleware> logger;

        private readonly string _defaultErrorCode;

        private readonly List<string> routePrefix = new List<string> { "/ready", "/healthz", "/metrics" };

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, string defaultErrorCode)
        {
            _next = next;
            this.logger = logger;
            _defaultErrorCode = defaultErrorCode;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            string acceptEncoding = HeadersHelper.GetHeadersValue(httpContext, "Accept-Encoding");
            string accept = HeadersHelper.GetHeadersValue(httpContext, "Accept");
            string contentLength = $"{httpContext.Request.ContentLength}";
            string? contentType = httpContext.Request.ContentType;
            string forwarded = HeadersHelper.GetHeadersValue(httpContext, "forwarded");
            string host = HeadersHelper.GetHeadersValue(httpContext, "Host");
            string uberTraceid = HeadersHelper.GetHeadersValue(httpContext, "uber-trace-id");
            string userAgent = HeadersHelper.GetHeadersValue(httpContext, "User-Agent");
            string xForwardedFor = HeadersHelper.GetHeadersValue(httpContext, "x-forwarded-for");
            string xForwardedHost = HeadersHelper.GetHeadersValue(httpContext, "x-forwarded-host");
            string xForwardedPort = HeadersHelper.GetHeadersValue(httpContext, "x-forwarded-port");
            string xForwardedPrefix = HeadersHelper.GetHeadersValue(httpContext, "x-forwarded-prefix");
            string xForwardedProto = HeadersHelper.GetHeadersValue(httpContext, "x-forwarded-proto");
            string xInputRequestId = HeadersHelper.GetHeadersValue(httpContext, HeadersHelper.XInputRequestId);
            string xInputTimeStamp = HeadersHelper.GetHeadersValue(httpContext, HeadersHelper.XInputTimeStamp);

            try
            {
                using (LogContext.PushProperty("x-input-request-id", xInputRequestId))
                using (LogContext.PushProperty("x-input-timestamp", xInputTimeStamp))
                using (LogContext.PushProperty("accept", accept))
                using (LogContext.PushProperty("accept-encoding", acceptEncoding))
                using (LogContext.PushProperty("content-type", contentType))
                using (LogContext.PushProperty("content-legth", contentLength))
                using (LogContext.PushProperty("host", host))
                using (LogContext.PushProperty("uber-trace-id", uberTraceid))
                using (LogContext.PushProperty("user-agent", userAgent))
                using (LogContext.PushProperty("forwarded", forwarded))
                using (LogContext.PushProperty("x-forwarded-for", xForwardedFor))
                using (LogContext.PushProperty("x-forwarded-host", xForwardedHost))
                using (LogContext.PushProperty("x-forwarded-port", xForwardedPort))
                using (LogContext.PushProperty("x-forwarded-prefix", xForwardedPrefix))
                using (LogContext.PushProperty("x-forwarded-proto", xForwardedProto))
                {
                    // Call the next delegate/middleware in the pipeline
                    await _next(httpContext);
                }


            }
            catch (Exception ex)
            {

                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private void PopulateHeaders(HttpContext httpContext, out string requestIdValue)
        {
            var inputRequestIdHeader = HeadersHelper.GetHadersFirstOrDefault(
                                                httpContext,
                                                HeadersHelper.XInputRequestId,
                                                HeadersHelper.XOutputRequestId,
                                                HeadersHelper.GenerateNewRequestId());

            if (!httpContext.Response.Headers.ContainsKey(inputRequestIdHeader.Key))
            {
                httpContext.Response.Headers.Add(inputRequestIdHeader);
            }
            if (!httpContext.Response.Headers.ContainsKey(HeadersHelper.XOutputTimeStamp))
            {
                httpContext.Response.Headers.Add(HeadersHelper.XOutputTimeStamp, HeadersHelper.ProcessTimeStamp());
            }

            requestIdValue = inputRequestIdHeader.Value;
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.ContentType = "application/json";
            string inpuRequestId;
            ErrorResponse errorObject;

            PopulateHeaders(httpContext, out inpuRequestId);

            switch (ex)
            {
                case GeneralException:
                    {
                        var generalException = ex as GeneralException;

                        if (generalException?.ExceptionType == ExceptionType.WARNING)
                        {
                            logger.LogWarning("General warning {errorCode} {reason} {message} {trackingId}",
                                    generalException.ErrorCode,
                                    generalException.Reason,
                                    generalException.Message,
                                    generalException.TrackingId
                                    );
                        }
                        else
                        {
                            logger.LogWarning(generalException,
                                    "General warning {errorCode} {reason} {message} {trackingId}",
                                    generalException!.ErrorCode,
                                    generalException.Reason,
                                    generalException.Message,
                                    generalException.TrackingId
                                    );
                        }

                        errorObject = new ErrorResponse
                        {
                            ErrorCode = generalException.ErrorCode,
                            Reason = generalException.Reason ?? "Data not found or Bad data.",
                            Message = generalException.Message,
                            TrackingId = generalException.TrackingId
                        };

                        return WriteResponseAndStatusCode(httpContext, errorObject, (int)generalException.StatusCode);
                    }
                case TaskCanceledException:
                    {
                        logger.LogWarning("A task was canceled.");

                        errorObject = new ErrorResponse
                        {
                            ErrorCode = _defaultErrorCode,
                            Reason = $"A task was canceled.",
                            Message = $"A task was canceled.",
                            TrackingId = Guid.NewGuid().ToString()
                        };

                        return WriteResponseAndStatusCode(httpContext, errorObject, (int)HttpStatusCode.RequestTimeout);
                    }
                default:
                    {
                        logger.LogError(ex, "System error {errorCode} {message}", _defaultErrorCode, ex.Message);

                        errorObject = new ErrorResponse
                        {
                            ErrorCode = _defaultErrorCode,
                            Reason = ex.Message + ex.InnerException?.Message,
                            Message = "System error.",
                            TrackingId = Guid.NewGuid().ToString()
                        };

                        return WriteResponseAndStatusCode(httpContext, errorObject, (int)HttpStatusCode.InternalServerError);
                    }
            }
        }

        private static Task WriteResponseAndStatusCode(HttpContext httpContext, ErrorResponse errorResponse, int httpStatusCode)
        {
            httpContext.Response.StatusCode = httpStatusCode;
            return httpContext.Response.WriteAsync(errorResponse.ToString());
        }
    }
}

