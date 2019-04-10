﻿// Copyright (C) WIFIPLUG. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WifiPlug.Api.Entities
{
    /// <summary>
    /// Represents a device service characteristic edit request.
    /// </summary>
    public class DeviceServiceCharacteristicEditEntity
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [JsonProperty(PropertyName = "value")]
        public object Value { get; set; }
    }
}
