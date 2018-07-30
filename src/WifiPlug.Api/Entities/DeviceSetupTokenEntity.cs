using System;
using System.Collections.Generic;
using System.Text;

namespace WifiPlug.Api.Entities
{
    /// <summary>
    /// Represents a device setup token.
    /// </summary>
    public class DeviceSetupTokenEntity
    {
        /// <summary>
        /// Gets or sets the UUID.
        /// </summary>
        public Guid UUID { get; set; }

        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        public string Token { get; set; }
    }
}
