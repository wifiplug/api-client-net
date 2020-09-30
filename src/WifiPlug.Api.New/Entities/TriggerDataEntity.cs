using Newtonsoft.Json;
using WifiPlug.Api.New.Schema;

namespace WifiPlug.Api.New.Entities
{
    public class TriggerDataEntity
    {
        #region Properties
        /// <summary>
        /// Gets or sets the resource type. See <see cref="TriggerResource" />.
        /// </summary>
        [JsonProperty("resourceType")]
        public string ResourceType { get; set; }

        /// <summary>
        /// Gets or sets the value type.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [JsonProperty("value")]
        public object Value { get; set; }
        #endregion
    }
}
