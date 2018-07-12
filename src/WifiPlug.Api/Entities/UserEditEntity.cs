using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WifiPlug.Api.Entities
{
    /// <summary>
    /// Represents a request to edit a user.
    /// </summary>
    public class UserEditEntity
    {
        /// <summary>
        /// Gets or sets the given (first) name.
        /// </summary>
        [JsonProperty(PropertyName = "given_name")]
        public string GivenName { get; set; }

        /// <summary>
        /// Gets or sets the family (last) name.
        /// </summary>
        [JsonProperty(PropertyName = "family_name")]
        public string FamilyName { get; set; }
    }
}
