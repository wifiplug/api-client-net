using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WifiPlug.Api.Entities
{
    /// <summary>
    /// Represents the result of a timer scan.
    /// </summary>
    public class TimerResultsEntity
    {
        /// <summary>
        /// Gets or sets the timer in the results.
        /// </summary>
        [JsonProperty(PropertyName = "timers")]
        public TimerEntity[] Timers { get; set; }

        /// <summary>
        /// Gets or sets the next cursor.
        /// </summary>
        [JsonProperty(PropertyName = "cursor")]
        public string Cursor { get; set; }

        /// <summary>
        /// Gets or sets the total number of timers.
        /// </summary>
        [JsonProperty(PropertyName = "total_timer_count")]
        public int TotalTimers { get; set; }
    }
}
