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
    public class SessionOperations
    {
        private ApiClient _client;

        /// <summary>
        /// Gets the current session information.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<SessionEntity> GetCurrentSesionAsync(CancellationToken cancellationToken = default(CancellationToken)) {
            return _client.RequestJsonSerializedAsync<SessionEntity>(HttpMethod.Get, "session", cancellationToken);
        }

        /// <summary>
        /// Deletes the current session (logout).
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task DeleteCurrentSessionAsync(CancellationToken cancellationToken = default(CancellationToken)) {
            return _client.RequestAsync(HttpMethod.Delete, "session", null, cancellationToken);
        }

        /// <summary>
        /// Login with the provided login entity.
        /// </summary>
        /// <param name="entity">The login entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<SessionEntity> LoginAsync(SessionLoginEntity entity, CancellationToken cancellationToken = default(CancellationToken)) {
            return _client.RequestJsonSerializedAsync<SessionLoginEntity, SessionEntity>(HttpMethod.Post, "session/login", entity, cancellationToken);
        }

        /// <summary>
        /// Login with the provided username and password.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<SessionEntity> LoginAsync(string username, string password, CancellationToken cancellationToken = default(CancellationToken)) {
            return _client.RequestJsonSerializedAsync<SessionLoginEntity, SessionEntity>(HttpMethod.Post, "session/login", new SessionLoginEntity() {
                Username = username,
                Password = password
            }, cancellationToken);
        }

        internal SessionOperations(ApiClient client) {
            _client = client;
        }
    }
}
