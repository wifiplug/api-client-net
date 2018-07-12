using System;
using System.Collections.Generic;
using System.Text;

namespace WifiPlug.Api.Schema
{
    /// <summary>
    /// Defines the characteristic types for the API.
    /// </summary>
    public static class Characteristics
    {
        /// <summary>
        /// If the service is turned on or off.
        /// </summary>
        public static readonly Guid PowerState = new Guid("a9e8093e-38cf-4758-bff9-a4b4ad9f0c81");

        /// <summary>
        /// The voltage (volts).
        /// </summary>
        public static readonly Guid Voltage = new Guid("b1c83641-18b2-4092-9f0a-ec3c28d8e644");

        /// <summary>
        /// The current (amperage).
        /// </summary>
        public static readonly Guid Current = new Guid("9bcaa8ce-efcb-4068-9c8f-4bb0093b6984");

        /// <summary>
        /// The power (wattage).
        /// </summary>
        public static readonly Guid Power = new Guid("c73c0220-a122-45c4-bc00-c4f88de34dbf");

        /// <summary>
        /// If the outlet is in use.
        /// </summary>
        public static readonly Guid OutletInUse = new Guid("a97c8c8e-16d1-415d-929a-670ce00a8a45");
    }
}
