using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WifiPlug.Api.Entities
{
    /// <summary>
    /// Represents a verification resource.
    /// </summary>
    public class VerificationEntity
    {
        /// <summary>
        /// Gets or sets the verification UUID.
        /// </summary>
        [JsonProperty(PropertyName = "verification_uuid")]
        public Guid VerificationUUID { get; set; }
    }
}
