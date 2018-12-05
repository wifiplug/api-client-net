// Copyright (C) WIFIPLUG. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

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
