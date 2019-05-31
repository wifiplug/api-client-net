using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WifiPlug.Api.Entities
{
    /// <summary>
    /// Defines a timer item resource.
    /// </summary>
    public class TimerItemResourceEntity
    {
        /// <summary>
        /// Gets or sets the UUID of the resource.
        /// </summary>
        [JsonProperty("uuid")]
        public Guid UUID { get; set; }

        /// <summary>
        /// Gets or sets the Service UUID if the resource is a device.
        /// </summary>
        [JsonProperty("service_uuid")]
        public Guid? ServiceUUID { get; set; }

        /// <summary>
        /// Gets or sets the Characteristic UUID if the resource is a device.
        /// </summary>
        [JsonProperty("characteristic_uuid")]
        public Guid? CharacteristicUUID { get; set; }
    }
}
