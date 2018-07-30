﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WifiPlug.Api.Operations;

namespace WifiPlug.Api
{
    /// <summary>
    /// Defines the interface for an api client.
    /// </summary>
    public interface IApiClient
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

        /// <summary>
        /// Pings the API to check if it's up.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The ping response.</returns>
        Task<string> PingAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
