using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WifiPlug.Api.Entities
{
    /// <summary>
    /// Represents an item in a group.
    /// </summary>
    public class GroupItemEntity
    {
        /// <summary>
        /// Gets or sets the UUID.
        /// </summary>
        [JsonProperty(PropertyName = "uuid")]
        public Guid UUID { get; set; }

        /// <summary>
        /// Gets or sets the device.
        /// </summary>
        [JsonProperty(PropertyName = "device")]
        public DeviceEntity Device { get; set; }

        /// <summary>
        /// Gets or sets the target service.
        /// </summary>
        [JsonProperty(PropertyName = "service")]
        public DeviceServiceEntity Service { get; set; }

        /// <summary>
        /// Gets or sets the target characteristic.
        /// </summary>
        [JsonProperty(PropertyName = "characteristic")]
        public DeviceServiceCharacteristicEntity Characteristic { get; set; }
    }
}
