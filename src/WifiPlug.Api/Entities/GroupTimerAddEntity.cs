using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WifiPlug.Api.Converters;
using WifiPlug.Api.Schema;

namespace WifiPlug.Api.Entities
{
    /// <summary>
    /// Represents a request to add a timer to a group.
    /// </summary>
    public class GroupTimerAddEntity
    {
        /// <summary>
        /// Gets or sets the next time the timer will run.
        /// </summary>
        [JsonProperty(PropertyName = "datetime")]
        [JsonConverter(typeof(InaccurateIsoDateTimeConverter))]
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Gets or sets the days the timer will repeat on, if any.
        /// </summary>
        [JsonProperty(PropertyName = "repeats", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(TimerRepetitionConverter))]
        public TimerRepetition Repeats { get; set; }

        /// <summary>
        /// Gets or sets the triggered action.
        /// </summary>
        [JsonProperty(PropertyName = "action")]
        public bool Action { get; set; }
    }
}
