// Copyright (C) WIFIPLUG. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace WifiPlug.Api.Entities
{
    /// <summary>
    /// Represents a item to trigger for a timer.
    /// </summary>
    public class TimerItemEntity
    {
        /// <summary>
        /// Gets or sets the item type.
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the item UUID.
        /// </summary>
        [JsonProperty(PropertyName = "uuid")]
        public Guid UUID { get; set; }

        /// <summary>
        /// Gets or sets the device UUID if the item is a device.
        /// </summary>
        [JsonProperty(PropertyName = "device_uuid")]
        public Guid? DeviceUUID { get; set; }

        /// <summary>
        /// Gets or sets the service UUID if the item is a device.
        /// </summary>
        [JsonProperty(PropertyName = "service_uuid")]
        public Guid? ServiceUUID { get; set; }

        /// <summary>
        /// Gets or sets the characteristic UUID if the item is a device.
        /// </summary>
        [JsonProperty(PropertyName = "characteristic_uuid")]
        public Guid? CharacteristicUUID { get; set; }

        /// <summary>
        /// Gets or sets the group UUID if the item is a group.
        /// </summary>
        [JsonProperty(PropertyName = "group_uuid")]
        public Guid? GroupUUID { get; set; }

        /// <summary>
        /// Check if the item is a device.
        /// </summary>
        /// <returns></returns>
        public bool IsDevice()
        {
            if (Type == "device")
                return true;
            return false;
        }

        /// <summary>
        /// Check if the item is a group.
        /// </summary>
        /// <returns></returns>
        public bool IsGroup()
        {
            if (Type == "group")
                return true;
            return false;
        }
    }
}
