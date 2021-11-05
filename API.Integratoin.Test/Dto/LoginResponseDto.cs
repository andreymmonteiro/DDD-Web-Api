using Newtonsoft.Json;
using System;

namespace API.Integration.Test.Dto
{
    public class LoginResponseDto
    {
        [JsonProperty("Authenticated")]
        public bool Authenticated { get; set;  }
        [JsonProperty("Created")]
        public DateTime Created { get; set; }
        [JsonProperty("ExpirationToken")]
        public DateTime ExpirationToken { get; set; }
        [JsonProperty("AcessToken")]
        public string AcessToken { get; set; }
        [JsonProperty("RefreshToken")]
        public string RefreshToken { get; set; }

        [JsonProperty("ExpirationRefreshToken")]
        public DateTime ExpirationRefreshToken { get; set; }
        [JsonProperty("Message")]
        public string Message { get; set; } = "Usuário Logado com sucesso";
    }
}
