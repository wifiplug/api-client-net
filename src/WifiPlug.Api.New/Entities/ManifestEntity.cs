using Newtonsoft.Json;
using System;

namespace WifiPlug.Api.New.Entities
{
    public class ManifestEntity
    {
        #region Properties
        /// <summary>
        /// Gets or sets the name of the manifest.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the manifest UUID.
        /// </summary>
        [JsonProperty("uuid")]
        public Guid UUID { get; set; }

        /// <summary>
        /// Gets or sets the variant.
        /// </summary>
        [JsonProperty("variant")]
        public string Variant { get; set; }

        /// <summary>
        /// Gets or sets the manifest version.
        /// </summary>
        [JsonProperty("version")]
        public string Version { get; set; }
        #endregion
    }
}
