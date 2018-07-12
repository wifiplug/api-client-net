using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WifiPlug.Api.Entities
{
    /// <summary>
    /// Represents a request to control a on/off target.
    /// </summary>
    public class ControlEntity
    {
        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        [JsonProperty(PropertyName = "state")]
        public bool State { get; set; }
    }
}
