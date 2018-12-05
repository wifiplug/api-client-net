// Copyright (C) WIFIPLUG. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Text;

namespace WifiPlug.Api.Schema
{
    /// <summary>
    /// Defines the possible grouping values for historical data.
    /// </summary>
    public enum HistoricalGrouping
    {
        /// <summary>
        /// Group historical data into minute averaged data points.
        /// </summary>
        Minute,

        /// <summary>
        /// Group historical data into hour averaged data points.
        /// </summary>
        Hour,

        /// <summary>
        /// Group historical data into day averaged data points.
        /// </summary>
        Day
    }
}