using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WifiPlug.Api.Entities
{
    /// <summary>
    /// Represents a request to edit an endpoints notification token.
    /// </summary>
    public class EndpointEditNotificationEntity
    {
        /// <summary>
        /// Gets or sets the push token.
        /// </summary>
        [JsonProperty("push_token")]
        public string PushToken { get; set; }
    }
}
