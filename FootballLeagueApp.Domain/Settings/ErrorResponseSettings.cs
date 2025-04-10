using FootballLeagueApp.Common.Interfaces;
using FootballLeagueApp.Common.Models.Exceptions;

namespace FootballLeagueApp.Domain.Settings
{
    public class ErrorResponseSettings : IDefaultErrorCodeProvider
    {
        string IDefaultErrorCodeProvider.DefaultValidationErrorCode { get ; set; } = ErrorCodes.BadDataErrorCode;
    }
}
