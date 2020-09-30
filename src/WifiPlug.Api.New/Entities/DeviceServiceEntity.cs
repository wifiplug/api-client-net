using Newtonsoft.Json;
using System;

namespace WifiPlug.Api.New.Entities
{
    public class DeviceServiceEntity
    {
        #region Properties
        /// <summary>
        /// Gets or sets the caption.
        /// </summary>
        [JsonProperty("caption")]
        public string Caption { get; set; }

        /// <summary>
        /// Gets or sets the characteristics.
        /// </summary>
        [JsonProperty("characteristics")]
        public DeviceServiceCharacteristicEntity[] Characteristics { get; set; }

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
        /// Gets or sets the type UUID.
        /// </summary>
        [JsonProperty("typeUuid")]
        public Guid TypeUUID { get; set; }

        /// <summary>
        /// Gets or sets the UUID.
        /// </summary>
        [JsonProperty("uuid")]
        public Guid UUID { get; set; }
        #endregion
    }
}
