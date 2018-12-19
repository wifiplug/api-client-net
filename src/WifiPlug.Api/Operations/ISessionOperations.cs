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
    /// Defines an interface for performing session operations.
    /// </summary>
    public interface ISessionOperations
    {
        /// <summary>
        /// Gets the current session information.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<SessionEntity> GetCurrentSesionAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
