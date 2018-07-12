using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WifiPlug.Api.Entities
{
    /// <summary>
    /// Represents historical energy consumption.
    /// </summary>
    public class HistoricalEnergyConsumptionEntity : EnergyConsumptionEntity
    {
        /// <summary>
        /// Gets or sets the reading timestamp.
        /// </summary>
        [JsonProperty(PropertyName = "time")]
        public DateTime Timestamp { get; set; }
    }
}
