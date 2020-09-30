using System;
using System.Threading;
using System.Threading.Tasks;
using WifiPlug.Api.New.Entities;

namespace WifiPlug.Api.New.Operations
{
    public interface ITriggerOperations
    {
        #region Public Methods
        /// <summary>
        /// Add a device trigger.
        /// </summary>
        /// <param name="deviceUuid">The UUID of the device.</param>
        /// <param name="trigger">The new trigger to add.</param>
        Task<TriggerEntity> AddDeviceTriggerAsync(Guid deviceUuid, TriggerAddEntity trigger, CancellationToken cancellationToken = default);

        /// <summary>
        /// Add a device trigger.
        /// </summary>
        /// <param name="deviceUuid">The UUID of the device.</param>
        /// <param name="trigger">The new trigger to add.</param>
        Task<TTriggerEntity> AddDeviceTriggerAsync<TTriggerEntity>(Guid deviceUuid, TriggerAddEntity trigger, CancellationToken cancellationToken = default)
            where TTriggerEntity : class;

        /// <summary>
        /// Delete a device trigger.
        /// </summary>
        /// <param name="deviceUuid">The UUID of the device.</param>
        /// <param name="triggerUuid">The UUID of the trigger.</param>
        Task DeleteDeviceTriggerAsync(Guid deviceUuid, Guid triggerUuid, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get a device trigger.
        /// </summary>
        /// <param name="deviceUuid">The UUID of the device.</param>
        /// <param name="triggerUuid">The UUID of the trigger.</param>
        Task<TriggerEntity> GetDeviceTriggerAsync(Guid deviceUuid, Guid triggerUuid, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get a device trigger.
        /// </summary>
        /// <param name="deviceUuid">The UUID of the device.</param>
        /// <param name="triggerUuid">The UUID of the trigger.</param>
        Task<TTriggerEntity> GetDeviceTriggerAsync<TTriggerEntity>(Guid deviceUuid, Guid triggerUuid, CancellationToken cancellationToken = default)
            where TTriggerEntity : class;

        /// <summary>
        /// Get all device triggers.
        /// </summary>
        /// <param name="deviceUuid">The UUID of the device.</param>
        Task<TriggerEntity[]> ListAllDeviceTriggersAsync(Guid deviceUuid, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get all device triggers.
        /// </summary>
        /// <param name="deviceUuid">The UUID of the device.</param>
        Task<TTriggerEntity[]> ListAllDeviceTriggersAsync<TTriggerEntity>(Guid deviceUuid, CancellationToken cancellationToken = default)
            where TTriggerEntity : class;

        /// <summary>
        /// Get a page of device triggers.
        /// </summary>
        /// <param name="deviceUuid">The UUID of the device.</param>
        Task<ResultResponseEntity<TriggerEntity>> ListDeviceTriggersAsync(Guid deviceUuid, string cursor = null, int limit = 50, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get a page of device triggers.
        /// </summary>
        /// <param name="deviceUuid">The UUID of the device.</param>
        Task<ResultResponseEntity<TTriggerEntity>> ListDeviceTriggersAsync<TTriggerEntity>(Guid deviceUuid, string cursor = null, int limit = 50, CancellationToken cancellationToken = default)
            where TTriggerEntity : class;
        #endregion
    }
}
