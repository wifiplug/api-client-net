// Copyright (C) WIFIPLUG. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WifiPlug.Api.Entities
{
    /// <summary>
    /// Represents the result of a group item scan.
    /// </summary>
    public class GroupItemResultsEntity
    {
        /// <summary>
        /// Gets or sets the items in the results.
        /// </summary>
        [JsonProperty(PropertyName = "items")]
        public GroupItemEntity[] Items { get; set; }

        /// <summary>
        /// Gets or sets the next cursor.
        /// </summary>
        [JsonProperty(PropertyName = "cursor")]
        public string Cursor { get; set; }

        /// <summary>
        /// Gets or sets the total number of group items.
        /// </summary>
        [JsonProperty(PropertyName = "total_item_count")]
        public int TotalItems { get; set; }
    }
}
