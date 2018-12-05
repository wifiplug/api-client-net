// Copyright (C) WIFIPLUG. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Text;

namespace WifiPlug.Api.Schema
{
    /// <summary>
    /// Defines the service types for the API.
    /// </summary>
    public static class Services
    {
        /// <summary>
        /// An outlet.
        /// </summary>
        public static readonly Guid Outlet = new Guid("1ea7e8ab-5981-4d9d-8343-3deae13b4fff");

        /// <summary>
        /// A switch.
        /// </summary>
        public static readonly Guid Switch = new Guid("5d956555-7890-4c07-b55f-a34e230b6f9c");

        /// <summary>
        /// An energy metering, usually linked to another service.
        /// </summary>
        public static readonly Guid EnergyMeter = new Guid("e4b8c402-e3f8-4ba3-8593-8565664a18bf");
    }
}
