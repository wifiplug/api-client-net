using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WifiPlug.Api.Entities
{
    /// <summary>
    /// Represents a request to add a notification token.
    /// </summary>
    public class UserNotificationAddEntity
    {
        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        [JsonProperty(PropertyName = "token")]
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the device type.
        /// </summary>
        [JsonProperty(PropertyName = "device_type")]
        public string DeviceType { get; set; }
    }
}
