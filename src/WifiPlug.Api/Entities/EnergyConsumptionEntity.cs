using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WifiPlug.Api.Entities
{
    /// <summary>
    /// Represents the total energy consumption.
    /// </summary>
    public class EnergyConsumptionEntity
    {
        /// <summary>
        /// Gets or sets the kilowatt hours.
        /// </summary>
        [JsonProperty(PropertyName = "kwh")]
        public float KilowattHours { get; set; }
    }
}
