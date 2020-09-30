using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WifiPlug.Api.New.Entities;
using WifiPlug.Api.New.Operations.Base;

namespace WifiPlug.Api.New.Operations
{
    public class DeviceOperations : BaseOperations, IDeviceOperations
    {
        #region Public Methods
        /// <summary>
        /// Edit a device.
        /// </summary>
        /// <param name="deviceUuid">The UUID of the device.</param>
        /// <param name="changes">The changes to make.</param>
        public virtual Task<DeviceEntity> EditDeviceAsync(Guid deviceUuid, DeviceEditEntity changes, CancellationToken cancellationToken = default)
            => ((IDeviceOperations)this).EditDeviceAsync<DeviceEntity>(deviceUuid, changes, cancellationToken);

        /// <summary>
        /// Edit a device.
        /// </summary>
        /// <param name="deviceUuid">The UUID of the device.</param>
        /// <param name="changes">The changes to make.</param>
        public virtual Task<TDeviceEntity> EditDeviceAsync<TDeviceEntity>(Guid deviceUuid, DeviceEditEntity changes, CancellationToken cancellationToken = default)
            where TDeviceEntity : class
            => ApiRequestor.RequestJsonSerializedAsync<DeviceEditEntity, TDeviceEntity>(HttpMethod.Post, $"device/{deviceUuid}", changes, cancellationToken);

        /// <summary>
        /// Get a device.
        /// </summary>
        /// <param name="deviceUuid">The UUID of the device.</param>
        public virtual Task<DeviceEntity> GetDeviceAsync(Guid deviceUuid, CancellationToken cancellationToken = default)
            => ((IDeviceOperations)this).GetDeviceAsync<DeviceEntity>(deviceUuid, cancellationToken);

        /// <summary>
        /// Get a device.
        /// </summary>
        /// <param name="deviceUuid">The UUID of the device.</param>
        public virtual Task<TDeviceEntity> GetDeviceAsync<TDeviceEntity>(Guid deviceUuid, CancellationToken cancellationToken = default)
            where TDeviceEntity : class
            => ApiRequestor.RequestJsonSerializedAsync<TDeviceEntity>(HttpMethod.Get, $"device/{deviceUuid}", cancellationToken);

        /// <summary>
        /// Get all devices.
        /// </summary>
        public virtual Task<DeviceEntity[]> ListAllDevicesAsync(CancellationToken cancellationToken = default)
            => ((IDeviceOperations)this).ListAllDevicesAsync<DeviceEntity>(cancellationToken);

        /// <summary>
        /// Get all devices.
        /// </summary>
        public virtual Task<TDeviceEntity[]> ListAllDevicesAsync<TDeviceEntity>(CancellationToken cancellationToken = default)
            where TDeviceEntity : class
            => ApiRequestor.RequestEntireListJsonSerializedAsync<TDeviceEntity>("device", cancellationToken);

        /// <summary>
        /// Get a page of devices.
        /// </summary>
        /// <param name="cursor">The page cursor.</param>
        /// <param name="limit">The page limit.</param>
        public virtual Task<ResultResponseEntity<DeviceEntity>> ListDevicesAsync(string cursor = null, int limit = 50, CancellationToken cancellationToken = default)
            => ((IDeviceOperations)this).ListDevicesAsync<DeviceEntity>(cursor, limit, cancellationToken);

        /// <summary>
        /// Get a page of devices.
        /// </summary>
        /// <param name="cursor">The page cursor.</param>
        /// <param name="limit">The page limit.</param>
        public virtual Task<ResultResponseEntity<TDeviceEntity>> ListDevicesAsync<TDeviceEntity>(string cursor = null, int limit = 50, CancellationToken cancellationToken = default)
            where TDeviceEntity : class
            => ApiRequestor.RequestResultResponseJsonSerializedAsync<TDeviceEntity>(cursor, limit, "device", cancellationToken);
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new set of API operations for devices.
        /// </summary>
        /// <param name="apiRequestor">The API requestor to use for communicating with the API.</param>
        public DeviceOperations(IApiRequestor apiRequestor)
            : base(apiRequestor)
        {
        }
        #endregion
    }
}
