using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WifiPlug.Api.Entities
{
    /// <summary>
    /// Represents a request to register a user.
    /// </summary>
    public class RegisterUserEntity
    {
        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        [JsonProperty(PropertyName = "email_address")]
        public string EmailAddress { get; set; }
    }
}
