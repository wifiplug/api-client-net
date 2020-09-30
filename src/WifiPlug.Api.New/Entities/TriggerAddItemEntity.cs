using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using WifiPlug.Api.New.Schema;

namespace WifiPlug.Api.New.Entities
{
    public class TriggerAddItemEntity
    {
        #region Properties
        /// <summary>
        /// Gets or sets the action to perform when the trigger is fired.
        /// </summary>
        [JsonProperty("action")]
        public bool Action { get; set; }

        /// <summary>
        /// Gets or sets the data, if any. See <see cref="TriggerResource" /> for keys.
        /// </summary>
        [JsonProperty("data")]
        public Dictionary<string, string> Data { get; set; }

        /// <summary>
        /// Gets or sets the resource type.
        /// </summary>
        [JsonProperty("resourceType", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string ResourceType { get; set; }

        /// <summary>
        /// Gets or sets the UUID of the target resource.
        /// </summary>
        [JsonProperty("resourceUuid")]
        public Guid ResourceUUID { get; set; }
        #endregion
    }
}
