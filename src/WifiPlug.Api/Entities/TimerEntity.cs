// Copyright (C) WIFIPLUG. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WifiPlug.Api.Converters;
using WifiPlug.Api.Schema;

namespace WifiPlug.Api.Entities
{
    /// <summary>
    /// Represents a timer.
    /// </summary>
    public class TimerEntity
    {
        /// <summary>
        /// Gets or sets the UUID.
        /// </summary>
        [JsonProperty(PropertyName = "uuid", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Guid UUID { get; set; }

        /// <summary>
        /// Gets or sets the next time the timer will run.
        /// </summary>
        [JsonProperty(PropertyName = "datetime")]
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Gets or sets the items that will be triggered.
        /// </summary>
        [JsonProperty(PropertyName = "items", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(TimerItemEntityConverter))]
        public TimerItemEntity[] Items { get; set; }

        /// <summary>
        /// Gets or sets the days the timer will repeat on, if any.
        /// </summary>
        [JsonProperty(PropertyName = "repeats")]
        [JsonConverter(typeof(TimerRepetitionConverter))]
        public TimerRepetition Repeats { get; set; }

        /// <summary>
        /// Gets or sets the triggered action.
        /// </summary>
        [JsonProperty(PropertyName = "action")]
        public bool Action { get; set; }

        /// <summary>
        /// Gets if the timer will repeat on the provided day.
        /// </summary>
        /// <param name="repetitionDay">The repetition day.</param>
        /// <returns>If the timer will repeat on the day.</returns>
        public bool ShouldRepeat(TimerRepetition repetitionDay) {
            return (Repeats & repetitionDay) == repetitionDay;
        }
    }
}
