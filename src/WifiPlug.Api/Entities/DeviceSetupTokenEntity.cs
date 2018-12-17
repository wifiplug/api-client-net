// Copyright (C) WIFIPLUG. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WifiPlug.Api.Entities
{
    /// <summary>
    /// Represents a device setup token.
    /// </summary>
    public class DeviceSetupTokenEntity
    {
        /// <summary>
        /// Gets or sets the UUID.
        /// </summary>
        [JsonProperty("uuid")]
        public Guid UUID { get; set; }

        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
