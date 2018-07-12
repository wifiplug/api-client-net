using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace WifiPlug.Api.Entities
{
    /// <summary>
    /// Represents a item to trigger for a timer.
    /// </summary>
    public class TimerItemEntity
    {
        /// <summary>
        /// Gets or sets the item type.
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the raw item entity.
        /// </summary>
        [JsonProperty(PropertyName = "entity")]
        public object Entity { get; set; }

        /// <summary>
        /// Gets the target entity as a device.
        /// </summary>
        /// <returns></returns>
        public DeviceEntity AsDevice() {
            if (!Type.Equals("device", StringComparison.CurrentCultureIgnoreCase))
                throw new InvalidOperationException("The timer item is not targetting a device");

            return (DeviceEntity)Entity;
        }

        /// <summary>
        /// Gets the target entity as a group.
        /// </summary>
        /// <returns></returns>
        public GroupEntity AsGroup() {
            if (!Type.Equals("group", StringComparison.CurrentCultureIgnoreCase))
                throw new InvalidOperationException("The timer item is not targetting a group");

            return (GroupEntity)Entity;
        }
    }
}
