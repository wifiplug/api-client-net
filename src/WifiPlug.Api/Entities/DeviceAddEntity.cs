// Copyright (C) WIFIPLUG. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WifiPlug.Api.Entities
{
    /// <summary>
    /// Represents a request to add a device, this is a legacy request and not supported by most API keys.
    /// </summary>
    public class DeviceAddEntity
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the MAC address.
        /// </summary>
        [JsonProperty(PropertyName = "mac_address")]
        public string MacAddress { get; set; }

        /// <summary>
        /// Gets or sets the type code.
        /// </summary>
        [JsonProperty(PropertyName = "type_code")]
        public int TypeCode { get; set; }

        /// <summary>
        /// Gets or sets the variant.
        /// </summary>
        [JsonProperty(PropertyName = "variant")]
        public string Variant { get; set; }

        /// <summary>
        /// Gets or sets the firmware version.
        /// </summary>
        [JsonProperty(PropertyName = "firmware_version")]
        public string FirmwareVersion { get; set; }
    }
}
