using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WifiPlug.Api.Entities
{
    /// <summary>
    /// Represents a request to activate a user.
    /// </summary>
    public class ActivateUserEntity
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
        /// Gets or sets the password.
        /// </summary>
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the given name.
        /// </summary>
        [JsonProperty(PropertyName = "given_name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string GivenName { get; set; }

        /// <summary>
        /// Gets or sets the family name.
        /// </summary>
        [JsonProperty(PropertyName = "family_name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string FamilyName { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        [JsonProperty(PropertyName = "phone", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string PhoneNumber { get; set; }
    }
}
