using Newtonsoft.Json;

namespace WifiPlug.Api.New.Entities
{
    public class TriggerRepeatsEntity
    {
        #region Properties
        /// <summary>
        /// Gets or sets whether to repeat on Friday.
        /// </summary>
        [JsonProperty("friday")]
        public bool Friday { get; set; }

        /// <summary>
        /// Gets or sets whether to repeat on Monday.
        /// </summary>
        [JsonProperty("monday")]
        public bool Monday { get; set; }

        /// <summary>
        /// Gets or sets whether to repeat on Saturday.
        /// </summary>
        [JsonProperty("saturday")]
        public bool Saturday { get; set; }

        /// <summary>
        /// Gets or sets whether to repeat on Sunday.
        /// </summary>
        [JsonProperty("sunday")]
        public bool Sunday { get; set; }

        /// <summary>
        /// Gets or sets whether to repeat on Thursday.
        /// </summary>
        [JsonProperty("thursday")]
        public bool Thursday { get; set; }

        /// <summary>
        /// Gets or sets whether to repeat on Tuesday.
        /// </summary>
        [JsonProperty("tuesday")]
        public bool Tuesday { get; set; }

        /// <summary>
        /// Gets or sets whether to repeat on Wednesday.
        /// </summary>
        [JsonProperty("wednesday")]
        public bool Wednesday { get; set; }
        #endregion
    }
}
