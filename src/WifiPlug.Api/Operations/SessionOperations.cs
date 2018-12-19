// Copyright (C) WIFIPLUG. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WifiPlug.Api.Authentication;
using WifiPlug.Api.Entities;

namespace WifiPlug.Api.Operations
{
    /// <summary>
    /// Provides operations for session resources.
    /// </summary>
    public class SessionOperations : ISessionOperations
    {
        /// <summary>
        /// The API client.
        /// </summary>
        protected IBaseApiRequestor _client;

        /// <summary>
        /// Gets the current session information.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<SessionEntity> GetCurrentSesionAsync(CancellationToken cancellationToken = default(CancellationToken)) {
            return _client.RequestJsonSerializedAsync<SessionEntity>(HttpMethod.Get, "session", cancellationToken);
        }

        /// <summary>
        /// Creates a session operations object.
        /// </summary>
        /// <param name="client">The client.</param>
        protected internal SessionOperations(BaseApiClient client) {
            _client = client;
        }
    }
}
