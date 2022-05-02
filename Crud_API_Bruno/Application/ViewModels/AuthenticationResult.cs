using System;
using System.Text.Json.Serialization;

namespace Crud_API_Bruno.Application.ViewModels
{
    public class AuthenticationResult
    {
        [JsonIgnore]
        public bool Success { get; set; }
        public string ReasonOfFail { get; set; }
        public string AccessToken { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}