using System;
using System.Net;

namespace FootballLeagueApp.Common.Exceptions.Models
{
    public class GeneralException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public string Reason { get; }

        public string TrackingId { get; }

        public string ExceptionLocation { get; }

        public string ErrorCode { get; }

        public ExceptionType ExceptionType { get; }
        public GeneralException(string exceptionLocation, 
            HttpStatusCode statusCode, 
            string errorCode, 
            string message, 
            ExceptionType exceptionType) : base(message)
        {
            ExceptionLocation = exceptionLocation;
            StatusCode = statusCode;
            ErrorCode = errorCode;
            Reason = message;
            TrackingId = Guid.NewGuid().ToString();
            ExceptionType = exceptionType;
        }

        public GeneralException(string exceptionLocation, 
            HttpStatusCode statusCode, 
            string errorCode, 
            string message, 
            string reason, 
            ExceptionType exceptionType, 
            string? trackingId
            ) : base(message)
        {
            ExceptionLocation = exceptionLocation;
            StatusCode = statusCode;
            ErrorCode = errorCode;
            Reason = reason;
            TrackingId = trackingId ?? Guid.NewGuid().ToString();
            ExceptionType = exceptionType;
        }
    }
}
