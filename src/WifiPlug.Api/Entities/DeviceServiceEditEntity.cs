using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WifiPlug.Api.Entities
{
    /// <summary>
    /// Represents the request entity for editing a device service.
    /// </summary>
    public class DeviceServiceEditEntity
    {
        /// <summary>
        /// Gets or sets the caption.
        /// </summary>
        [JsonProperty(PropertyName = "caption", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Caption { get; set; }
    }
}
