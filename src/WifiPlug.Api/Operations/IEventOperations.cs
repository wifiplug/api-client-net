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
    /// Defines an interface for performing event operations.
    /// </summary>
    public interface IEventOperations
    {
        /// <summary>
        /// Gets a event by UUID.
        /// </summary>
        /// <param name="eventUuid">The UUID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The event.</returns>
        Task<EventEntity> GetEventAsync(Guid eventUuid, CancellationToken cancellationToken = default(CancellationToken));
    }
}
