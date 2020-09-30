using Newtonsoft.Json;
using System;

namespace WifiPlug.Api.New.Entities
{
    public class DeviceEntity
    {
        #region Properties
        /// <summary>
        /// Gets or sets when the device was created.
        /// </summary>
        [JsonProperty("createdAt")]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets whether the device is online.
        /// </summary>
        [JsonProperty("isOnline")]
        public bool IsOnline { get; set; }

        /// <summary>
        /// Gets or sets when the device was last updated.
        /// </summary>
        [JsonProperty("lastUpdated")]
        public DateTimeOffset LastUpdated { get; set; }

        /// <summary>
        /// Gets or sets the device's MAC address.
        /// </summary>
        [JsonProperty("macAddress")]
        public string MacAddress { get; set; }

        /// <summary>
        /// Gets or sets the device's manifest information.
        /// </summary>
        [JsonProperty("manifest")]
        public ManifestEntity Manifest { get; set; }

        /// <summary>
        /// Gets or sets the device's model.
        /// </summary>
        [JsonProperty("model")]
        public string Model { get; set; }

        /// <summary>
        /// Gets or sets the device's name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the device type.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the device's UUID.
        /// </summary>
        [JsonProperty("uuid")]
        public Guid UUID { get; set; }
        #endregion
    }
}
