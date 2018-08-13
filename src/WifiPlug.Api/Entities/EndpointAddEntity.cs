using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WifiPlug.Api.Entities
{
    /// <summary>
    /// Represents an endpoint add entity.
    /// </summary>
    public class EndpointAddEntity
    {
        /// <summary>
        /// Gets or sets the application UUID.
        /// </summary>
        [JsonProperty("application_uuid")]
        public Guid ApplicationUUID { get; set; }

        /// <summary>
        /// Gets or sets the device model.
        /// </summary>
        [JsonProperty("device_model")]
        public string DeviceModel { get; set; }

        /// <summary>
        /// Gets or sets the device operating system.
        /// </summary>
        [JsonProperty("device_os")]
        public string DeviceOS { get; set; }

        /// <summary>
        /// Gets or sets the 2-letter country code.
        /// </summary>
        [JsonProperty("country")]
        public string Country { get; set; }
    }
}
