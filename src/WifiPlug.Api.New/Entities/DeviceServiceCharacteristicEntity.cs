using Newtonsoft.Json;
using System;

namespace WifiPlug.Api.New.Entities
{
    public class DeviceServiceCharacteristicEntity
    {
        #region Properties
        /// <summary>
        /// Gets or sets when the service was created.
        /// </summary>
        [JsonProperty("createdAt")]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets when the service was last updated.
        /// </summary>
        [JsonProperty("lastUpdated")]
        public DateTimeOffset LastUpdated { get; set; }

        /// <summary>
        /// Gets or sets the value type.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the type UUID.
        /// </summary>
        [JsonProperty("typeUuid")]
        public Guid TypeUUID { get; set; }

        /// <summary>
        /// Gets or sets the UUID.
        /// </summary>
        [JsonProperty("uuid")]
        public Guid UUID { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [JsonProperty("value")]
        public object Value { get; set; }
        #endregion
    }
}
