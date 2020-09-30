using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WifiPlug.Api.New.Entities;
using WifiPlug.Api.New.Operations.Base;

namespace WifiPlug.Api.New.Operations
{
    public class DeviceTriggerOperations : BaseOperations, IDeviceTriggerOperations
    {
        #region Public Methods
        /// <summary>
        /// Add a device trigger.
        /// </summary>
        /// <param name="deviceUuid">The UUID of the device.</param>
        /// <param name="trigger">The new trigger to add.</param>
        public virtual Task<TriggerEntity> AddDeviceTriggerAsync(Guid deviceUuid, TriggerAddEntity trigger, CancellationToken cancellationToken = default)
            => ((IDeviceTriggerOperations)this).AddDeviceTriggerAsync<TriggerEntity>(deviceUuid, trigger, cancellationToken);

        /// <summary>
        /// Add a device trigger.
        /// </summary>
        /// <param name="deviceUuid">The UUID of the device.</param>
        /// <param name="trigger">The new trigger to add.</param>
        public virtual Task<TTriggerEntity> AddDeviceTriggerAsync<TTriggerEntity>(Guid deviceUuid, TriggerAddEntity trigger, CancellationToken cancellationToken = default)
            where TTriggerEntity : class
            => ApiRequestor.RequestJsonSerializedAsync<TriggerAddEntity, TTriggerEntity>(HttpMethod.Post, $"device/{deviceUuid}/trigger/add", trigger, cancellationToken);

        /// <summary>
        /// Delete a device trigger.
        /// </summary>
        /// <param name="deviceUuid">The UUID of the device.</param>
        /// <param name="triggerUuid">The UUID of the trigger.</param>
        public virtual Task DeleteDeviceTriggerAsync(Guid deviceUuid, Guid triggerUuid, CancellationToken cancellationToken = default)
            => ApiRequestor.RequestAsync(HttpMethod.Delete, $"device/{deviceUuid}/trigger/{triggerUuid}", null, cancellationToken);

        /// <summary>
        /// Disable a device trigger.
        /// </summary>
        /// <param name="deviceUuid">The UUID of the device.</param>
        /// <param name="triggerUuid">The UUID of the trigger.</param>
        public virtual Task<TriggerEntity> DisableDeviceTriggerAsync(Guid deviceUuid, Guid triggerUuid, CancellationToken cancellationToken = default)
            => ((IDeviceTriggerOperations)this).DisableDeviceTriggerAsync<TriggerEntity>(deviceUuid, triggerUuid, cancellationToken);

        /// <summary>
        /// Disable a device trigger.
        /// </summary>
        /// <param name="deviceUuid">The UUID of the device.</param>
        /// <param name="triggerUuid">The UUID of the trigger.</param>
        public virtual Task<TTriggerEntity> DisableDeviceTriggerAsync<TTriggerEntity>(Guid deviceUuid, Guid triggerUuid, CancellationToken cancellationToken = default)
            where TTriggerEntity : class
            => ApiRequestor.RequestJsonSerializedAsync<TTriggerEntity>(HttpMethod.Post, $"device/{deviceUuid}/trigger/{triggerUuid}/disable", cancellationToken);

        /// <summary>
        /// Enable a device trigger.
        /// </summary>
        /// <param name="deviceUuid">The UUID of the device.</param>
        /// <param name="triggerUuid">The UUID of the trigger.</param>
        public virtual Task<TriggerEntity> EnableDeviceTriggerAsync(Guid deviceUuid, Guid triggerUuid, CancellationToken cancellationToken = default)
            => ((IDeviceTriggerOperations)this).EnableDeviceTriggerAsync<TriggerEntity>(deviceUuid, triggerUuid, cancellationToken);

        /// <summary>
        /// Enable a device trigger.
        /// </summary>
        /// <param name="deviceUuid">The UUID of the device.</param>
        /// <param name="triggerUuid">The UUID of the trigger.</param>
        public virtual Task<TTriggerEntity> EnableDeviceTriggerAsync<TTriggerEntity>(Guid deviceUuid, Guid triggerUuid, CancellationToken cancellationToken = default)
            where TTriggerEntity : class
            => ApiRequestor.RequestJsonSerializedAsync<TTriggerEntity>(HttpMethod.Post, $"device/{deviceUuid}/trigger/{triggerUuid}/enable", cancellationToken);

        /// <summary>
        /// Get a trigger.
        /// </summary>
        /// <param name="deviceUuid">The UUID of the device.</param>
        /// <param name="triggerUuid">The UUID of the trigger.</param>
        public virtual Task<TriggerEntity> GetDeviceTriggerAsync(Guid deviceUuid, Guid triggerUuid, CancellationToken cancellationToken = default)
            => ((IDeviceTriggerOperations)this).GetDeviceTriggerAsync(deviceUuid, triggerUuid, cancellationToken);

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
            => ((IDeviceTriggerOperations)this).ListAllDeviceTriggersAsync<TriggerEntity>(deviceUuid, cancellationToken);

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
            => ((IDeviceTriggerOperations)this).ListDeviceTriggersAsync<TriggerEntity>(deviceUuid, cursor, limit, cancellationToken);

        /// <summary>
        /// Get a page of device trigger.
        /// </summary>
        /// <param name="deviceUuid">The UUID of the device.</param>
        public virtual Task<ResultResponseEntity<TTriggerEntity>> ListDeviceTriggersAsync<TTriggerEntity>(Guid deviceUuid, string cursor = null, int limit = 50, CancellationToken cancellationToken = default)
            where TTriggerEntity : class
            => ApiRequestor.RequestResultResponseJsonSerializedAsync<TTriggerEntity>(cursor, limit, $"device/{deviceUuid}/trigger", cancellationToken);

        /// <summary>
        /// Toggle the enabled state of a device trigger.
        /// </summary>
        /// <param name="deviceUuid">The UUID of the device.</param>
        /// <param name="triggerUuid">The UUID of the trigger.</param>
        /// <param name="enabled">Whether to enable or disable the trigger. If not provided the trigger will be swapped to the opposite state.</param>
        public virtual Task<TriggerEntity> ToggleDeviceTriggerAsync(Guid deviceUuid, Guid triggerUuid, bool? enabled = null, CancellationToken cancellationToken = default)
            => ((IDeviceTriggerOperations)this).ToggleDeviceTriggerAsync<TriggerEntity>(deviceUuid, triggerUuid, enabled);

        /// <summary>
        /// Toggle the enabled state of a device trigger.
        /// </summary>
        /// <param name="deviceUuid">The UUID of the device.</param>
        /// <param name="triggerUuid">The UUID of the trigger.</param>
        /// <param name="enabled">Whether to enable or disable the trigger. If not provided the trigger will be swapped to the opposite state.</param>
        public virtual Task<TTriggerEntity> ToggleDeviceTriggerAsync<TTriggerEntity>(Guid deviceUuid, Guid triggerUuid, bool? enabled = null, CancellationToken cancellationToken = default)
            where TTriggerEntity : class
        {
            var path = $"device/{deviceUuid}/trigger/{triggerUuid}/toggle";

            if (enabled.HasValue)
                path += $"?enabled={enabled.Value}";

            return ApiRequestor.RequestJsonSerializedAsync<TTriggerEntity>(HttpMethod.Post, path, cancellationToken);
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new set of API operations for timers.
        /// </summary>
        /// <param name="apiRequestor">The API requestor to use for communicating with the API.</param>
        public DeviceTriggerOperations(IApiRequestor apiRequestor)
            : base(apiRequestor)
        {
        }
        #endregion
    }
}
