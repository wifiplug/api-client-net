// Copyright (C) WIFIPLUG. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WifiPlug.Api.Entities
{
    /// <summary>
    /// Represents the result of a group scan.
    /// </summary>
    public class GroupResultsEntity
    {
        /// <summary>
        /// Gets or sets the groups.
        /// </summary>
        [JsonProperty(PropertyName = "groups")]
        public GroupEntity[] Groups { get; set; }

        /// <summary>
        /// Gets or sets the next cursor.
        /// </summary>
        [JsonProperty(PropertyName = "cursor")]
        public string Cursor { get; set; }

        /// <summary>
        /// Gets or sets the total number of groups.
        /// </summary>
        [JsonProperty(PropertyName = "total_group_count")]
        public int TotalGroups { get; set; }
    }
}
