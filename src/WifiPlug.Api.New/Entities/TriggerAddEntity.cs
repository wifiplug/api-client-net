using Newtonsoft.Json;
using System.Collections.Generic;
using WifiPlug.Api.New.Schema;

namespace WifiPlug.Api.New.Entities
{
    public class TriggerAddEntity
    {
        #region Properties
        /// <summary>
        /// Gets or sets the data, if any. See <see cref="TriggerType" /> for keys.
        /// </summary>
        [JsonProperty("data")]
        public Dictionary<string, string> Data { get; set; }

        /// <summary>
        /// Gets or sets the trigger's items, if any.
        /// </summary>
        [JsonProperty("items")]
        public TriggerAddItemEntity[] Items { get; set; }

        /// <summary>
        /// Gets or sets when the timer repeats.
        /// </summary>
        [JsonProperty("repeats", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public TriggerRepeatsEntity Repeats { get; set; }

        /// <summary>
        /// Gets or sets the trigger type.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }
        #endregion
    }
}
