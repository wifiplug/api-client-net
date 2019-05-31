using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WifiPlug.Api.Converters;
using WifiPlug.Api.Schema;

namespace WifiPlug.Api.Entities
{
    /// <summary>
    /// Represents a request to edit a timer on a device.
    /// </summary>
    public class DeviceTimerEditEntity
    {
        /// <summary>
        /// Gets or sets the next time the timer will run.
        /// </summary>
        [JsonProperty(PropertyName = "datetime", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime? DateTime { get; set; }

        /// <summary>
        /// Gets or sets the days the timer will repeat on, if any.
        /// </summary>
        [JsonProperty(PropertyName = "repeats", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(TimerRepetitionConverter))]
        public TimerRepetition? Repeats { get; set; }

        /// <summary>
        /// Gets or sets the service UUID.
        /// </summary>
        [JsonProperty(PropertyName = "service_uuid", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Guid? ServiceUUID { get; set; }

        /// <summary>
        /// Gets or sets the characteristic UUID.
        /// </summary>
        [JsonProperty(PropertyName = "characteristic_uuid", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Guid? CharacteristicUUID { get; set; }

        /// <summary>
        /// Gets or sets the triggered action.
        /// </summary>
        [JsonProperty(PropertyName = "action", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool? Action { get; set; }
    }
}