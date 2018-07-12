using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WifiPlug.Api.Entities
{
    /// <summary>
    /// Represents a historical energy reading.
    /// </summary>
    public class HistoricalEnergyReadingEntity : EnergyReadingEntity
    {
        /// <summary>
        /// Gets or sets the reading timestamp.
        /// </summary>
        [JsonProperty(PropertyName = "time")]
        public DateTime Timestamp { get; set; }
    }
}
