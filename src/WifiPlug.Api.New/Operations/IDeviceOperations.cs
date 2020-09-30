using System;
using System.Threading;
using System.Threading.Tasks;
using WifiPlug.Api.New.Entities;

namespace WifiPlug.Api.New.Operations
{
    public interface IDeviceOperations
    {
        #region Public Methods
        /// <summary>
        /// Edit a device.
        /// </summary>
        /// <param name="deviceUuid">The UUID of the device.</param>
        /// <param name="changes">The changes to make.</param>
        Task<DeviceEntity> EditDeviceAsync(Guid deviceUuid, DeviceEditEntity changes, CancellationToken cancellationToken = default);

        /// <summary>
        /// Edit a device.
        /// </summary>
        /// <param name="deviceUuid">The UUID of the device.</param>
        /// <param name="changes">The changes to make.</param>
        Task<TDeviceEntity> EditDeviceAsync<TDeviceEntity>(Guid deviceUuid, DeviceEditEntity changes, CancellationToken cancellationToken = default)
            where TDeviceEntity : class;

        /// <summary>
        /// Get a device.
        /// </summary>
        /// <param name="deviceUuid">The UUID of the device.</param>
        Task<DeviceEntity> GetDeviceAsync(Guid deviceUuid, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get a device.
        /// </summary>
        /// <param name="deviceUuid">The UUID of the device.</param>
        Task<TDeviceEntity> GetDeviceAsync<TDeviceEntity>(Guid deviceUuid, CancellationToken cancellationToken = default)
            where TDeviceEntity : class;

        /// <summary>
        /// Get all devices.
        /// </summary>
        Task<DeviceEntity[]> ListAllDevicesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Get all devices.
        /// </summary>
        Task<TDeviceEntity[]> ListAllDevicesAsync<TDeviceEntity>(CancellationToken cancellationToken = default)
            where TDeviceEntity : class;

        /// <summary>
        /// Get a page of devices.
        /// </summary>
        /// <param name="cursor">The page cursor.</param>
        /// <param name="limit">The page limit.</param>
        Task<ResultResponseEntity<DeviceEntity>> ListDevicesAsync(string cursor = null, int limit = 50, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get a page of devices.
        /// </summary>
        /// <param name="cursor">The page cursor.</param>
        /// <param name="limit">The page limit.</param>
        Task<ResultResponseEntity<TDeviceEntity>> ListDevicesAsync<TDeviceEntity>(string cursor = null, int limit = 50, CancellationToken cancellationToken = default)
            where TDeviceEntity : class;
        #endregion
    }
}
