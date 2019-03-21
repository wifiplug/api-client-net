// Copyright (C) WIFIPLUG. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WifiPlug.Api.Entities
{
    /// <summary>
    /// Represents the result of a device scan.
    /// </summary>
    public class DeviceResultsEntity
    {
        /// <summary>
        /// Gets or sets the devices.
        /// </summary>
        [JsonProperty(PropertyName = "devices")]
        public DeviceEntity[] Devices { get; set; }

        /// <summary>
        /// Gets or sets the next cursor.
        /// </summary>
        [JsonProperty(PropertyName = "cursor")]
        public string Cursor { get; set; }

        /// <summary>
        /// Gets or sets the total number of devices.
        /// </summary>
        [JsonProperty(PropertyName = "total_device_count")]
        public long TotalDevices { get; set; }
    }
}
