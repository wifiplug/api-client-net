// Copyright (C) WIFIPLUG. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WifiPlug.Api.Entities
{
    /// <summary>
    /// Represents a request to edit an endpoints notification token.
    /// </summary>
    public class EndpointEditNotificationEntity
    {
        /// <summary>
        /// Gets or sets the push token.
        /// </summary>
        [JsonProperty("push_token")]
        public string PushToken { get; set; }
    }
}
