// Copyright (C) WIFIPLUG. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WifiPlug.Api.Entities
{
    /// <summary>
    /// Represents the result of a event scan.
    /// </summary>
    public class EventResultsEntity
    {
        /// <summary>
        /// Gets or sets the events.
        /// </summary>
        [JsonProperty(PropertyName = "events")]
        public EventEntity[] Events { get; set; }

        /// <summary>
        /// Gets or sets the next cursor.
        /// </summary>
        [JsonProperty(PropertyName = "cursor")]
        public string Cursor { get; set; }

        /// <summary>
        /// Gets or sets the total number of events.
        /// </summary>
        [JsonProperty(PropertyName = "total_event_count")]
        public int TotalEvents { get; set; }
    }
}
