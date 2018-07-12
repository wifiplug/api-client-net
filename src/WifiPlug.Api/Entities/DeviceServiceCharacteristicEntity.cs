using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WifiPlug.Api.Entities
{
    /// <summary>
    /// Represents a device service characteristic resource.
    /// </summary>
    public class DeviceServiceCharacteristicEntity
    {
        /// <summary>
        /// Gets or sets the UUID.
        /// </summary>
        [JsonProperty(PropertyName = "uuid")]
        public Guid UUID { get; set; }

        /// <summary>
        /// Gets or sets the type UUID.
        /// </summary>
        [JsonProperty(PropertyName = "type_uuid")]
        public Guid TypeUUID { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [JsonProperty(PropertyName = "value")]
        public object Value { get; set; }
    }
}
