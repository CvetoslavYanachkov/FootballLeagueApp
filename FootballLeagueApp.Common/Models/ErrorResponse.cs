using System.Text.Json;

namespace FootballLeagueApp.Common.Common.Models
{
    public class ErrorResponse
    {
        public string? ErrorCode { get; set; }

        public string Reason { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public string? TrackingId { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
