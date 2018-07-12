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

        /// <summary>
        /// Deletes the current session (logout).
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task DeleteCurrentSessionAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Login with the provided login entity.
        /// </summary>
        /// <param name="entity">The login entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<SessionEntity> LoginAsync(SessionLoginEntity entity, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Login with the provided username and password.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <remarks>This operation is internal and won't work with normal API keys. Nor is it stable.</remarks>
        /// <returns></returns>
        Task<SessionEntity> LoginAsync(string username, string password, CancellationToken cancellationToken = default(CancellationToken));
    }
}
