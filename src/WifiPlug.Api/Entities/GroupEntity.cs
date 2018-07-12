using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WifiPlug.Api.Entities
{
    /// <summary>
    /// Represents a group.
    /// </summary>
    public class GroupEntity
    {
        /// <summary>
        /// Gets or sets the UUID.
        /// </summary>
        [JsonProperty(PropertyName = "uuid")]
        public Guid UUID { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the item count.
        /// </summary>
        [JsonProperty(PropertyName = "item_count")]
        public int ItemCount { get; set; }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        [JsonProperty(PropertyName = "items")]
        public GroupItemEntity[] Items { get; set; }
    }
}
