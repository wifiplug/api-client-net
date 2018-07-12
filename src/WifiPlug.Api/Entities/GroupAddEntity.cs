using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WifiPlug.Api.Entities
{
    /// <summary>
    /// Represents a request to add a group.
    /// </summary>
    public class GroupAddEntity
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the items to be added.
        /// </summary>
        [JsonProperty(PropertyName = "items")]
        public GroupAddItemEntity[] Items { get; set; }
    }
}
