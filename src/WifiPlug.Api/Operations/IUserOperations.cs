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

        /// <summary>
        /// Changes the password of the currently authenticated user.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <remarks>This is an internal API and is not guarenteed to be stable between versions.</remarks>
        /// <returns>The user entity.</returns>
        Task ChangePasswordAsync(ChangePasswordEntity entity, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Changes the password of the currently authenticated user.
        /// </summary>
        /// <param name="currentPassword">The current password.</param>
        /// <param name="newPassword">The new password.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <remarks>This is an internal API and is not guarenteed to be stable between versions.</remarks>
        /// <returns>The user entity.</returns>
        Task ChangePasswordAsync(string currentPassword, string newPassword, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Requests a forgotton password email.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <remarks>This is an internal API and is not guarenteed to be stable between versions.</remarks>
        /// <returns></returns>
        Task<VerificationEntity> ForgotPasswordAsync(ForgotPasswordEntity entity, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Requests a forgotton password email.
        /// </summary>
        /// <param name="email">The email address.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <remarks>This is an internal API and is not guarenteed to be stable between versions.</remarks>
        /// <returns></returns>
        Task<VerificationEntity> ForgotPasswordAsync(string email, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Resets a password with a code received by email.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <remarks>This is an internal API and is not guarenteed to be stable between versions.</remarks>
        /// <returns></returns>
        Task ResetPasswordAsync(ResetPasswordEntity entity, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Registers a user, sending an activation email to the provided address.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <remarks>This is an internal API and is not guarenteed to be stable between versions.</remarks>
        /// <returns></returns>
        Task<VerificationEntity> RegisterUserAsync(RegisterUserEntity entity, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Registers a user, sending an activation email to the provided address.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="cancellationToken"></param>
        /// <remarks>This is an internal API and is not guarenteed to be stable between versions.</remarks>
        /// <returns></returns>
        Task<VerificationEntity> RegisterUserAsync(string email, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Activates a user registration from the code provided in an email.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <remarks>This is an internal API and is not guarenteed to be stable between versions.</remarks>
        /// <returns></returns>
        Task<UserEntity> ActivateUserAsync(ActivateUserEntity entity, CancellationToken cancellationToken = default(CancellationToken));
    }
}
