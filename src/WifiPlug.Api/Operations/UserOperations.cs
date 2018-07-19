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
        protected ApiClient _client;

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
        /// Changes the password of the currently authenticated user.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <remarks>This operation is internal and won't work with normal API keys. Nor is it stable.</remarks>
        /// <returns>The user entity.</returns>
        public Task ChangePasswordAsync(ChangePasswordEntity entity, CancellationToken cancellationToken = default(CancellationToken)) {
            return _client.RequestJsonSerializedAsync(HttpMethod.Post, "user/password", entity, cancellationToken);
        }

        /// <summary>
        /// Changes the password of the currently authenticated user.
        /// </summary>
        /// <param name="currentPassword">The current password.</param>
        /// <param name="newPassword">The new password.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <remarks>This operation is internal and won't work with normal API keys. Nor is it stable.</remarks>
        /// <returns>The user entity.</returns>
        public Task ChangePasswordAsync(string currentPassword, string newPassword, CancellationToken cancellationToken = default(CancellationToken)) {
            return ChangePasswordAsync(new ChangePasswordEntity() {
                CurrentPassword = currentPassword,
                NewPassword = newPassword
            }, cancellationToken);
        }

        /// <summary>
        /// Requests a forgotton password email.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <remarks>This operation is internal and won't work with normal API keys. Nor is it stable.</remarks>
        /// <returns></returns>
        public Task<VerificationEntity> ForgotPasswordAsync(ForgotPasswordEntity entity, CancellationToken cancellationToken = default(CancellationToken)) {
            return _client.RequestJsonSerializedAsync<ForgotPasswordEntity, VerificationEntity>(HttpMethod.Post, "user/forgot_password", entity, cancellationToken);
        }

        /// <summary>
        /// Requests a forgotton password email.
        /// </summary>
        /// <param name="email">The email address.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <remarks>This operation is internal and won't work with normal API keys. Nor is it stable.</remarks>
        /// <returns></returns>
        public Task<VerificationEntity> ForgotPasswordAsync(string email, CancellationToken cancellationToken = default(CancellationToken)) {
            return ForgotPasswordAsync(new ForgotPasswordEntity() {
                EmailAddress = email
            }, cancellationToken);
        }

        /// <summary>
        /// Resets a password with a code received by email.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <remarks>This operation is internal and won't work with normal API keys. Nor is it stable.</remarks>
        /// <returns></returns>
        public Task ResetPasswordAsync(ResetPasswordEntity entity, CancellationToken cancellationToken = default(CancellationToken)) {
            return _client.RequestJsonSerializedAsync(HttpMethod.Post, "user/reset_password", entity, cancellationToken);
        }

        /// <summary>
        /// Registers a user, sending an activation email to the provided address.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <remarks>This operation is internal and won't work with normal API keys. Nor is it stable.</remarks>
        /// <returns></returns>
        public Task<VerificationEntity> RegisterUserAsync(RegisterUserEntity entity, CancellationToken cancellationToken = default(CancellationToken)) {
            return _client.RequestJsonSerializedAsync<RegisterUserEntity, VerificationEntity>(HttpMethod.Post, "user/register", entity, cancellationToken);
        }

        /// <summary>
        /// Registers a user, sending an activation email to the provided address.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="cancellationToken"></param>
        /// <remarks>This operation is internal and won't work with normal API keys. Nor is it stable.</remarks>
        /// <returns></returns>
        public Task<VerificationEntity> RegisterUserAsync(string email, CancellationToken cancellationToken = default(CancellationToken)) {
            return RegisterUserAsync(new RegisterUserEntity() {
                EmailAddress = email
            }, cancellationToken);
        }

        /// <summary>
        /// Activates a user registration from the code provided in an email.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <remarks>This operation is internal and won't work with normal API keys. Nor is it stable.</remarks>
        /// <returns></returns>
        public Task<UserEntity> ActivateUserAsync(ActivateUserEntity entity, CancellationToken cancellationToken = default(CancellationToken)) {
            return _client.RequestJsonSerializedAsync<ActivateUserEntity, UserEntity>(HttpMethod.Post, "user/activate", entity, cancellationToken);
        }

        /// <summary>
        /// Adds a user notification token to the current user.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <remarks>This operation is internal and won't work with normal API keys. Nor is it stable.</remarks>
        /// <returns></returns>
        public Task AddUserNotificationAsync(UserNotificationAddEntity entity, CancellationToken cancellationToken = default(CancellationToken)) {
            return _client.RequestJsonSerializedAsync(HttpMethod.Post, "user/notification/add", entity, cancellationToken);
        }

        internal UserOperations(ApiClient client) {
            _client = client;
        }
    }
}
