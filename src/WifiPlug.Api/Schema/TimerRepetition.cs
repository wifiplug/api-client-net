// Copyright (C) WIFIPLUG. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Text;

namespace WifiPlug.Api.Schema
{
    /// <summary>
    /// Defines the possible timer repetition days.
    /// </summary>
    [Flags]
    public enum TimerRepetition
    {
        /// <summary>
        /// Repeats on no days.
        /// </summary>
        None = 0,

        /// <summary>
        /// Monday.
        /// </summary>
        Monday = 1,

        /// <summary>
        /// Tuesday.
        /// </summary>
        Tuesday = 2,

        /// <summary>
        /// Wednesday.
        /// </summary>
        Wednesday = 4,

        /// <summary>
        /// Thursday.
        /// </summary>
        Thursday = 8,

        /// <summary>
        /// Friday.
        /// </summary>
        Friday = 16,

        /// <summary>
        /// Saturday.
        /// </summary>
        Saturday = 32,

        /// <summary>
        /// Sunday.
        /// </summary>
        Sunday = 64
    }
}
