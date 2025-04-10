using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace FootballLeagueApp.Common.Common.Models
{
    public class HeaderViewModel
    {
        [FromHeader(Name = "X-Input_Request_Id")]
        [JsonPropertyName("X-Input_Request_Id")]
        public string X_InputRequestId { get; set; }

        [FromHeader(Name = "X-Input_Timestamp")]
        [JsonPropertyName("X-Input_Timestamp")]
        public string X_InputTimestamp { get;  set; }
    }
}
