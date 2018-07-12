using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WifiPlug.Api.Entities
{
    /// <summary>
    /// Represents a request to reset the password.
    /// </summary>
    public class ResetPasswordEntity
    {
        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        [JsonProperty(PropertyName = "email_address")]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the verification UUID.
        /// </summary>
        [JsonProperty(PropertyName = "verification_uuid")]
        public Guid VerificationUUID { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the new password.
        /// </summary>
        [JsonProperty(PropertyName = "new_password")]
        public string NewPassword { get; set; }
    }
}
