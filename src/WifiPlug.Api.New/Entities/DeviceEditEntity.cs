using Newtonsoft.Json;

namespace WifiPlug.Api.New.Entities
{
    public class DeviceEditEntity
    {
        #region Properties
        /// <summary>
        /// Gets or sets the new name of the device.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        #endregion
    }
}
