﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WifiPlug.Api.Entities;

namespace WifiPlug.Api.Operations
{
    /// <summary>
    /// Provides operations for event resources.
    /// </summary>
    public class EventOperations : IEventOperations
    {
        /// <summary>
        /// The API client.
        /// </summary>
        protected ApiClient _client;
        
        /// <summary>
        /// Gets a event by UUID.
        /// </summary>
        /// <param name="eventUuid">The UUID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The event.</returns>
        public Task<EventEntity> GetEventAsync(Guid eventUuid, CancellationToken cancellationToken = default(CancellationToken)) {
            return _client.RequestJsonSerializedAsync<EventEntity>(HttpMethod.Get, $"event/{eventUuid}", cancellationToken);
        }

        /// <summary>
        /// Creates a event operations object.
        /// </summary>
        /// <param name="client">The client.</param>
        protected internal EventOperations(ApiClient client) {
            _client = client;
        }
    }
}
