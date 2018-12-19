// Copyright (C) WIFIPLUG. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WifiPlug.Api
{
    /// <summary>
    /// Defines the interface for an api client.
    /// </summary>
    public interface IBaseApiClient
    {
        /// <summary>
        /// Gets or sets the retry count for transient failures.
        /// </summary>
        int RetryCount { get; set; }

        /// <summary>
        /// Gets or sets the retry delay.
        /// </summary>
        TimeSpan RetryDelay { get; set; }

        /// <summary>
        /// Gets or sets the authentication method.
        /// </summary>
        ApiAuthentication Authentication { get; set; }

        /// <summary>
        /// Gets or sets the timeout for requests.
        /// </summary>
        TimeSpan Timeout { get; set; }

        /// <summary>
        /// Gets the underlying http client.
        /// </summary>
        HttpClient Client { get; }

        /// <summary>
        /// Gets or sets the base address.
        /// </summary>
        Uri BaseAddress { get; set; }
    }
}
