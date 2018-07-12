using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WifiPlug.Api.Entities
{
    /// <summary>
    /// Represents a request to change the users password.
    /// </summary>
    public class ChangePasswordEntity
    {
        /// <summary>
        /// Gets or sets the current password.
        /// </summary>
        [JsonProperty(PropertyName = "current_password")]
        public string CurrentPassword { get; set; }

        /// <summary>
        /// Gets or sets the new password.
        /// </summary>
        [JsonProperty(PropertyName = "new_password")]
        public string NewPassword { get; set; }
    }
}
