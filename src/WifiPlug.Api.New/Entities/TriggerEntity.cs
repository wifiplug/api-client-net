using Newtonsoft.Json;
using System;
using WifiPlug.Api.New.Schema;

namespace WifiPlug.Api.New.Entities
{
    public class TriggerEntity
    {
        #region Properties
        /// <summary>
        /// Gets or sets when the trigger was created.
        /// </summary>
        [JsonProperty("createdAt")]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the trigger's data, if any.
        /// </summary>
        [JsonProperty("data")]
        public TriggerDataEntity[] Data { get; set; }

        /// <summary>
        /// Gets or sets the trigger's items, if any.
        /// </summary>
        [JsonProperty("items")]
        public TriggerItemEntity[] Items { get; set; }

        /// <summary>
        /// Gets or sets when the trigger was last updated.
        /// </summary>
        [JsonProperty("lastUpdated")]
        public DateTimeOffset LastUpdated { get; set; }

        /// <summary>
        /// Gets or sets when the trigger repeats.
        /// </summary>
        [JsonProperty("repeats")]
        public TriggerRepeatsEntity Repeats { get; set; }

        /// <summary>
        /// Gets or sets the trigger type. See <see cref="TriggerType" />.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the UUID.
        /// </summary>
        [JsonProperty("uuid")]
        public Guid UUID { get; set; }
        #endregion
    }
}
