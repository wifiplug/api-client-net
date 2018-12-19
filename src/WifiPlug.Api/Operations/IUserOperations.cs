// Copyright (C) WIFIPLUG. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WifiPlug.Api.Entities;

namespace WifiPlug.Api.Operations
{
    /// <summary>
    /// Defines an interface for performing user operations.
    /// </summary>
    public interface IUserOperations
    {
        /// <summary>
        /// Edits the currently authenticated user.
        /// </summary>
        /// <param name="entity">The update entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<UserEntity> EditCurrentUserAsync(UserEditEntity entity, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the currently authenticated user.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The user entity.</returns>
        Task<UserEntity> GetCurrentUserAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
