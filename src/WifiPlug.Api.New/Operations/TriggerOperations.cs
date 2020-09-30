using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WifiPlug.Api.New.Entities;
using WifiPlug.Api.New.Operations.Base;

namespace WifiPlug.Api.New.Operations
{
    public class TriggerOperations : BaseOperations, ITriggerOperations
    {
        #region Public Methods
        /// <summary>
        /// Add a device trigger.
        /// </summary>
        /// <param name="deviceUuid">The UUID of the device.</param>
        /// <param name="trigger">The new trigger to add.</param>
        public Task<TriggerEntity> AddDeviceTriggerAsync(Guid deviceUuid, TriggerAddEntity trigger, CancellationToken cancellationToken = default)
            => ((ITriggerOperations)this).AddDeviceTriggerAsync<TriggerEntity>(deviceUuid, trigger, cancellationToken);

        /// <summary>
        /// Add a device trigger.
        /// </summary>
        /// <param name="deviceUuid">The UUID of the device.</param>
        /// <param name="trigger">The new trigger to add.</param>
        public Task<TTriggerEntity> AddDeviceTriggerAsync<TTriggerEntity>(Guid deviceUuid, TriggerAddEntity trigger, CancellationToken cancellationToken = default)
            where TTriggerEntity : class
            => ApiRequestor.RequestJsonSerializedAsync<TriggerAddEntity, TTriggerEntity>(HttpMethod.Post, $"device/{deviceUuid}/trigger/add", trigger, cancellationToken);

        /// <summary>
        /// Delete a device trigger.
        /// </summary>
        /// <param name="deviceUuid">The UUID of the device.</param>
        /// <param name="triggerUuid">The UUID of the trigger.</param>
        public Task DeleteDeviceTriggerAsync(Guid deviceUuid, Guid triggerUuid, CancellationToken cancellationToken = default)
            => ApiRequestor.RequestAsync(HttpMethod.Delete, $"device/{deviceUuid}/trigger/{triggerUuid}", null, cancellationToken);

        /// <summary>
        /// Get a trigger.
        /// </summary>
        /// <param name="deviceUuid">The UUID of the device.</param>
        /// <param name="triggerUuid">The UUID of the trigger.</param>
        public virtual Task<TriggerEntity> GetDeviceTriggerAsync(Guid deviceUuid, Guid triggerUuid, CancellationToken cancellationToken = default)
            => ((ITriggerOperations)this).GetDeviceTriggerAsync(deviceUuid, triggerUuid, cancellationToken);

        /// <summary>
        /// Get a timer.
        /// </summary>
        /// <param name="deviceUuid">The UUID of the device.</param>
        /// <param name="triggerUuid">The UUID of the timer.</param>
        public virtual Task<TTriggerEntity> GetDeviceTriggerAsync<TTriggerEntity>(Guid deviceUuid, Guid triggerUuid, CancellationToken cancellationToken = default)
            where TTriggerEntity : class
            => ApiRequestor.RequestJsonSerializedAsync<TTriggerEntity>(HttpMethod.Get, $"device/{deviceUuid}/trigger/{triggerUuid}", cancellationToken);

        /// <summary>
        /// Get all device trigger.
        /// </summary>
        /// <param name="deviceUuid">The UUID of the device.</param>
        public virtual Task<TriggerEntity[]> ListAllDeviceTriggersAsync(Guid deviceUuid, CancellationToken cancellationToken = default)
            => ((ITriggerOperations)this).ListAllDeviceTriggersAsync<TriggerEntity>(deviceUuid, cancellationToken);

        /// <summary>
        /// Get all device trigger.
        /// </summary>
        /// <param name="deviceUuid">The UUID of the device.</param>
        public virtual Task<TTriggerEntity[]> ListAllDeviceTriggersAsync<TTriggerEntity>(Guid deviceUuid, CancellationToken cancellationToken = default)
            where TTriggerEntity : class
            => ApiRequestor.RequestEntireListJsonSerializedAsync<TTriggerEntity>($"device/{deviceUuid}/trigger", cancellationToken);

        /// <summary>
        /// Get a page of device trigger.
        /// </summary>
        /// <param name="deviceUuid">The UUID of the device.</param>
        public virtual Task<ResultResponseEntity<TriggerEntity>> ListDeviceTriggersAsync(Guid deviceUuid, string cursor = null, int limit = 50, CancellationToken cancellationToken = default)
            => ((ITriggerOperations)this).ListDeviceTriggersAsync<TriggerEntity>(deviceUuid, cursor, limit, cancellationToken);

        /// <summary>
        /// Get a page of device trigger.
        /// </summary>
        /// <param name="deviceUuid">The UUID of the device.</param>
        public virtual Task<ResultResponseEntity<TTriggerEntity>> ListDeviceTriggersAsync<TTriggerEntity>(Guid deviceUuid, string cursor = null, int limit = 50, CancellationToken cancellationToken = default)
            where TTriggerEntity : class
            => ApiRequestor.RequestResultResponseJsonSerializedAsync<TTriggerEntity>(cursor, limit, $"device/{deviceUuid}/trigger", cancellationToken);
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new set of API operations for timers.
        /// </summary>
        /// <param name="apiRequestor">The API requestor to use for communicating with the API.</param>
        public TriggerOperations(IApiRequestor apiRequestor)
            : base(apiRequestor)
        {
        }
        #endregion
    }
}
