// Copyright (C) WIFIPLUG. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WifiPlug.Api.Entities
{
    /// <summary>
    /// Represents the result of a device user scan.
    /// </summary>
    public class DeviceUserResultsEntity
    {
        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        [JsonProperty(PropertyName = "users")]
        public DeviceUserEntity[] Users { get; set; }

        /// <summary>
        /// Gets or sets the next cursor.
        /// </summary>
        [JsonProperty(PropertyName = "cursor")]
        public string Cursor { get; set; }

        /// <summary>
        /// Gets or sets the total number of device users.
        /// </summary>
        [JsonProperty(PropertyName = "total_user_count")]
        public int TotalUsers { get; set; }
    }
}
