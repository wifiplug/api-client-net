using WifiPlug.Api.New.Operations;

namespace WifiPlug.Api.New
{
    public interface IApiClient
    {
        #region Properties
        /// <summary>
        /// Gets the API operations for devices.
        /// </summary>
        IDeviceOperations Devices { get; }

        /// <summary>
        /// Gets the API operations for triggers.
        /// </summary>
        ITriggerOperations Triggers { get; }
        #endregion
    }
}
