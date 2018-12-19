// Copyright (C) WIFIPLUG. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Text;
using WifiPlug.Api.Operations;

namespace WifiPlug.Api
{
    /// <summary>
    /// Defines the interface for a basic API client.
    /// </summary>
    public interface IApiClient : IBaseApiClient
    {
        /// <summary>
        /// Gets the device operations.
        /// </summary>
        IDeviceOperations Devices { get; }

        /// <summary>
        /// Gets the session operations.
        /// </summary>
        ISessionOperations Sessions { get; }

        /// <summary>
        /// Gets the user operations.
        /// </summary>
        IUserOperations Users { get; }

        /// <summary>
        /// Gets the group operations.
        /// </summary>
        IGroupOperations Groups { get; }

        /// <summary>
        /// Gets the event operations.
        /// </summary>
        IEventOperations Events { get; }
    }
}
