﻿// Copyright (C) WIFIPLUG. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WifiPlug.Api.Entities
{
    /// <summary>
    /// Represents a device user.
    /// </summary>
    public class DeviceUserEntity
    {
        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        [JsonProperty(PropertyName = "user")]
        public UserEntity User { get; set; }

        /// <summary>
        /// Gets or sets if the user is the owner.
        /// </summary>
        [JsonProperty(PropertyName = "owner")]
        public bool IsOwner { get; set; }
    }
}
