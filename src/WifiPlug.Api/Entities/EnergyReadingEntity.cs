using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace WifiPlug.Api.Entities
{
    /// <summary>
    /// Represents an energy reading.
    /// </summary>
    [DebuggerDisplay("Voltage: {Voltage}V Current: {Current}A Power: {Power}W")]
    public class EnergyReadingEntity
    {
        /// <summary>
        /// Gets or sets the voltage.
        /// </summary>
        [JsonProperty(PropertyName = "voltage")]
        public float Voltage { get; set; }

        /// <summary>
        /// Gets or sets the current.
        /// </summary>
        [JsonProperty(PropertyName = "current")]
        public float Current { get; set; }

        /// <summary>
        /// Gets or sets the power (wattage).
        /// </summary>
        [JsonProperty(PropertyName = "power")]
        public float Power { get; set; }
    }
}
