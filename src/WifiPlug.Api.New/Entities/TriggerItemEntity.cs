using Newtonsoft.Json;
using System;

namespace WifiPlug.Api.New.Entities
{
    public class TriggerItemEntity
    {
        #region Properties
        /// <summary>
        /// Gets or sets the action to perform when the trigger is fired.
        /// </summary>
        [JsonProperty("action")]
        public bool Action { get; set; }

        /// <summary>
        /// Gets or sets when the trigger item was created.
        /// </summary>
        [JsonProperty("createdAt")]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the data, if any.
        /// </summary>
        [JsonProperty("data")]
        public TriggerDataEntity[] Data { get; set; }

        /// <summary>
        /// Gets or sets when the trigger item was last updated.
        /// </summary>
        [JsonProperty("lastUpdated")]
        public DateTimeOffset LastUpdated { get; set; }

        /// <summary>
        /// Gets or sets the resource type.
        /// </summary>
        [JsonProperty("resourceType")]
        public string ResourceType { get; set; }

        /// <summary>
        /// Gets or sets the UUID of the target resource.
        /// </summary>
        [JsonProperty("resourceUuid")]
        public Guid ResourceUUID { get; set; }

        /// <summary>
        /// Gets or sets the UUID.
        /// </summary>
        [JsonProperty("uuid")]
        public Guid UUID { get; set; }
        #endregion
    }
}
