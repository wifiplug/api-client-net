// Copyright (C) WIFIPLUG. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WifiPlug.Api.Authentication;
using WifiPlug.Api.Entities;
using WifiPlug.Api.Operations;

namespace WifiPlug.Api
{
    /// <summary>
    /// Provides access to the WIFIPLUG API services and systems.
    /// </summary>
    public class ApiClient : BaseApiClient, IApiClient
    {
        #region Fields
        private DeviceOperations _deviceOperations;
        private SessionOperations _sessionOperations;
        private UserOperations _userOperations;
        private GroupOperations _groupOperations;
        private EventOperations _eventOperations;
        #endregion

        #region Operations
        /// <summary>
        /// Gets the device operations.
        /// </summary>
        public override IDeviceOperations Devices {
            get {
                return _deviceOperations;
            }
        }

        /// <summary>
        /// Gets the session operations.
        /// </summary>
        public override ISessionOperations Sessions {
            get {
                return _sessionOperations;
            }
        }

        /// <summary>
        /// Gets the user operations.
        /// </summary>
        public override IUserOperations Users {
            get {
                return _userOperations;
            }
        }


        /// <summary>
        /// Gets the group operations.
        /// </summary>
        public override IGroupOperations Groups {
            get {
                return _groupOperations;
            }
        }


        /// <summary>
        /// Gets the event operations.
        /// </summary>
        public override IEventOperations Events {
            get {
                return _eventOperations;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new WIFIPLUG client without a API key or secret.
        /// </summary>
        public ApiClient() {
            _deviceOperations = new DeviceOperations(this);
            _sessionOperations = new SessionOperations(this);
            _userOperations = new UserOperations(this);
            _groupOperations = new GroupOperations(this);
            _eventOperations = new EventOperations(this);
        }

        /// <summary>
        /// Creates a new WIFIPLUG client without a API key or secret.
        /// </summary>
        /// <param name="apiUrl">The custom base path of the API.</param>
        public ApiClient(string apiUrl) : base(apiUrl) {
            _deviceOperations = new DeviceOperations(this);
            _sessionOperations = new SessionOperations(this);
            _userOperations = new UserOperations(this);
            _groupOperations = new GroupOperations(this);
            _eventOperations = new EventOperations(this);
        }

        /// <summary>
        /// Create a new WIFIPLUG client.
        /// </summary>
        /// <param name="apiKey">The API key.</param>
        /// <param name="apiSecret">The API secret.</param>
        public ApiClient(string apiKey, string apiSecret) : base(apiKey, apiSecret) {
            _deviceOperations = new DeviceOperations(this);
            _sessionOperations = new SessionOperations(this);
            _userOperations = new UserOperations(this);
            _groupOperations = new GroupOperations(this);
            _eventOperations = new EventOperations(this);
        }

        /// <summary>
        /// Create a new WIFIPLUG client.
        /// </summary>
        /// <param name="apiUrl">The custom base path of the API.</param>
        /// <param name="apiKey">The API key.</param>
        /// <param name="apiSecret">The API secret.</param>
        public ApiClient(string apiUrl, string apiKey, string apiSecret) : base(apiUrl, apiKey, apiSecret) {
            _deviceOperations = new DeviceOperations(this);
            _sessionOperations = new SessionOperations(this);
            _userOperations = new UserOperations(this);
            _groupOperations = new GroupOperations(this);
            _eventOperations = new EventOperations(this);
        }
        #endregion
    }
}
