﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WifiPlug.Api.Entities
{
    /// <summary>
    /// Represents a request to boost.
    /// </summary>
    public class BoostEntity
    {
        /// <summary>
        /// Gets or sets the number of seconds to keep the controllable resource on.
        /// </summary>
        [JsonProperty(PropertyName = "duration")]
        public int Duration { get; set; }
    }
}
