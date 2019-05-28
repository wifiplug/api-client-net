using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WifiPlug.Api.Entities
{
    public class OAuth2TokenResponseEntity
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("refreshable_until")]
        public DateTime RefreshableUntil { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }
    }
}
