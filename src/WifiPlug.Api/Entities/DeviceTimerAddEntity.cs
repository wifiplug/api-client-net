﻿// Copyright (C) WIFIPLUG. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;
using WifiPlug.Api.Converters;
using WifiPlug.Api.Schema;

namespace WifiPlug.Api.Entities
{
    /// <summary>
    /// Represents a request to add a timer to a device.
    /// </summary>
    public class DeviceTimerAddEntity
    {
        /// <summary>
        /// Gets or sets the next time the timer will run.
        /// </summary>
        [JsonProperty(PropertyName = "datetime")]
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Gets or sets the days the timer will repeat on, if any.
        /// </summary>
        [JsonProperty(PropertyName = "repeats", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(TimerRepetitionConverter))]
        public TimerRepetition Repeats { get; set; }

        /// <summary>
        /// Gets or sets the service UUID.
        /// </summary>
        [JsonProperty(PropertyName = "service_uuid")]
        public Guid ServiceUUID { get; set; }

        /// <summary>
        /// Gets or sets the characteristic UUID.
        /// </summary>
        [JsonProperty(PropertyName = "characteristic_uuid")]
        public Guid? Characteristic { get; set; }

        /// <summary>
        /// Gets or sets the triggered action.
        /// </summary>
        [JsonProperty(PropertyName = "action")]
        public bool Action { get; set; }
    }
}
