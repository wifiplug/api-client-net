using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WifiPlug.Api.Entities
{
    /// <summary>
    /// Represents a device entity.
    /// </summary>
    public class DeviceEntity
    {
        /// <summary>
        /// Gets or sets the UUID.
        /// </summary>
        [JsonProperty(PropertyName = "uuid", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Guid UUID { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [JsonProperty(PropertyName = "name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the mac address.
        /// </summary>
        [JsonProperty(PropertyName = "mac_address", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string MacAddress { get; set; }

        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        [JsonProperty(PropertyName = "model", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Model { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        [JsonProperty(PropertyName = "type", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the variant, always 8-digit hexidecimal.
        /// </summary>
        [JsonProperty(PropertyName = "variant", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Variant { get; set; }

        /// <summary>
        /// Gets or sets the number of users attached to the device.
        /// </summary>
        [JsonProperty(PropertyName = "user_count", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int UserCount { get; set; }

        /// <summary>
        /// Gets or sets the firmware version.
        /// </summary>
        [JsonProperty(PropertyName = "firmware_version", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string FirmwareVersion { get; set; }

        /// <summary>
        /// Gets or sets the services, may be null.
        /// </summary>
        [JsonProperty(PropertyName = "services", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DeviceServiceEntity[] Services { get; set; }

        /// <summary>
        /// Gets or sets if the device is online.
        /// </summary>
        [JsonProperty(PropertyName = "is_online", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool IsOnline { get; set; }
    }
}
