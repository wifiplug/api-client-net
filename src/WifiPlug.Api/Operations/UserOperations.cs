// Copyright (C) WIFIPLUG. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WifiPlug.Api.Entities;

namespace WifiPlug.Api.Operations
{
    /// <summary>
    /// Provides operations for user resources.
    /// </summary>
    public class UserOperations : IUserOperations
    {
        /// <summary>
        /// The API client.
        /// </summary>
        protected IBaseApiRequestor _client;

        /// <summary>
        /// Edits the currently authenticated user.
        /// </summary>
        /// <param name="entity">The update entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<UserEntity> EditCurrentUserAsync(UserEditEntity entity, CancellationToken cancellationToken = default(CancellationToken)) {
            return _client.RequestJsonSerializedAsync<UserEditEntity, UserEntity>(HttpMethod.Post, "user", entity, cancellationToken);
        }

        /// <summary>
        /// Gets the currently authenticated user.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The user entity.</returns>
        public Task<UserEntity> GetCurrentUserAsync(CancellationToken cancellationToken = default(CancellationToken)) {
            return _client.RequestJsonSerializedAsync<UserEntity>(HttpMethod.Get, "user", cancellationToken);
        }

        /// <summary>
        /// Creates a user operations object.
        /// </summary>
        /// <param name="client">The client.</param>
        protected internal UserOperations(IBaseApiRequestor client) {
            _client = client;
        }
    }
}
