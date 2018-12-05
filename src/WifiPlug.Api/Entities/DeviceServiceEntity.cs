// Copyright (C) WIFIPLUG. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WifiPlug.Api.Entities
{
    /// <summary>
    /// Represents a device service resource.
    /// </summary>
    public class DeviceServiceEntity
    {
        /// <summary>
        /// Gets or sets the UUID.
        /// </summary>
        [JsonProperty(PropertyName = "uuid")]
        public Guid UUID { get; set; }

        /// <summary>
        /// Gets or sets the type UUID.
        /// </summary>
        [JsonProperty(PropertyName = "type_uuid")]
        public Guid TypeUUID { get; set; }

        /// <summary>
        /// Gets or sets the captino.
        /// </summary>
        [JsonProperty(PropertyName = "caption")]
        public string Caption { get; set; }

        /// <summary>
        /// Gets or sets the characteristics.
        /// </summary>
        [JsonProperty(PropertyName = "characteristics")]
        public DeviceServiceCharacteristicEntity[] Characteristics { get; set; }
    }
}
