// Copyright (C) WIFIPLUG. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WifiPlug.Api.Entities
{
    /// <summary>
    /// Represents an item to be added to a group.
    /// </summary>
    public class GroupAddItemEntity
    {
        /// <summary>
        /// Gets or sets the device UUID.
        /// </summary>
        [JsonProperty(PropertyName = "device_uuid")]
        public Guid DeviceUUID { get; set; }

        /// <summary>
        /// Gets or sets the target service UUID.
        /// </summary>
        [JsonProperty(PropertyName = "service_uuid")]
        public Guid ServiceUUID { get; set; }

        /// <summary>
        /// Gets or sets the target characteristic UUID.
        /// </summary>
        [JsonProperty(PropertyName = "characteristic_uuid")]
        public Guid CharacteristicUUID { get; set; }
    }
}
