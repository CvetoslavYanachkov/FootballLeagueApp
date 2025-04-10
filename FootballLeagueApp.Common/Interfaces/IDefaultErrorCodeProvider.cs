using System.ComponentModel;

namespace FootballLeagueApp.Common.Interfaces
{
    public interface IDefaultErrorCodeProvider
    {
        [Description("Default error code response when request didnt pass validation rules.")]
        string DefaultValidationErrorCode { get; set; }
    }
}
