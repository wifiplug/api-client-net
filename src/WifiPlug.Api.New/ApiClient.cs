﻿using System;
using WifiPlug.Api.New.Operations;

namespace WifiPlug.Api.New
{
    public class ApiClient : BaseApiClient, IApiClient
    {
        #region Fields
        private readonly IDeviceOperations _devices;
        private readonly IDeviceTriggerOperations _deviceTriggers;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the API operations for devices.
        /// </summary>
        public virtual IDeviceOperations Devices => _devices;

        /// <summary>
        /// Gets the API operations for device triggers.
        /// </summary>
        public virtual IDeviceTriggerOperations DeviceTriggers => _deviceTriggers;
        #endregion

        #region Protected Methods
        protected virtual IDeviceOperations ConstructDeviceOperations()
            => new DeviceOperations(this);

        protected virtual IDeviceTriggerOperations ConstructDeviceTriggerOperations()
            => new DeviceTriggerOperations(this);
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new WIFIPLUG API client with an API key and secret.
        /// </summary>
        /// <param name="apiKey">Your WIFIPLUG API key.</param>
        /// <param name="apiSecret">Your WIFIPLUG API secret.</param>
        public ApiClient(string apiKey, string apiSecret)
            : this(null, apiKey, apiSecret)
        {
        }

        /// <summary>
        /// Creates a new WIFIPLUG API client with a custom base URI and an API key and secret.
        /// </summary>
        /// <param name="baseApiUri">The base URI to use for the API.</param>
        /// <param name="apiKey">Your WIFIPLUG API key.</param>
        /// <param name="apiSecret">Your WIFIPLUG API secret.</param>
        public ApiClient(Uri baseApiUri, string apiKey, string apiSecret)
            : base(baseApiUri, apiKey, apiSecret)
        {
            _devices = ConstructDeviceOperations();
            _deviceTriggers = ConstructDeviceTriggerOperations();
        }
        #endregion
    }
}
