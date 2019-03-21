// Copyright (C) WIFIPLUG. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Text;

namespace WifiPlug.Api.Schema
{
    /// <summary>
    /// Defines the characteristic types for the API.
    /// </summary>
    [Obsolete("Use DeviceFramework.Characteristic")]
    public static class Characteristics
    {
        /// <summary>
        /// If the service is turned on or off.
        /// </summary>
        [Obsolete("Use WifiPlug.DeviceFramework.Characteristic.PowerState")]
        public static readonly Guid PowerState = new Guid("a9e8093e-38cf-4758-bff9-a4b4ad9f0c81");

        /// <summary>
        /// The voltage (volts).
        /// </summary>
        [Obsolete("Use WifiPlug.DeviceFramework.Characteristic.Voltage")]
        public static readonly Guid Voltage = new Guid("b1c83641-18b2-4092-9f0a-ec3c28d8e644");

        /// <summary>
        /// The current (amperage).
        /// </summary>
        [Obsolete("Use WifiPlug.DeviceFramework.Characteristic.Current")]
        public static readonly Guid Current = new Guid("9bcaa8ce-efcb-4068-9c8f-4bb0093b6984");

        /// <summary>
        /// The power (wattage).
        /// </summary>
        [Obsolete("Use WifiPlug.DeviceFramework.Characteristic.Wattage")]
        public static readonly Guid Power = new Guid("c73c0220-a122-45c4-bc00-c4f88de34dbf");

        /// <summary>
        /// If the outlet is in use.
        /// </summary>
        [Obsolete("Use WifiPlug.DeviceFramework.Characteristic.InUse")]
        public static readonly Guid OutletInUse = new Guid("5f9982cb-aaeb-4d05-9e03-1a739a47fba0");
    }
}
