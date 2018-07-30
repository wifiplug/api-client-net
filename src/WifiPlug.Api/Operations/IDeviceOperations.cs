using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WifiPlug.Api.Entities;
using WifiPlug.Api.Schema;

namespace WifiPlug.Api.Operations
{
    /// <summary>
    /// Defines an interface for performing device operations.
    /// </summary>
    public interface IDeviceOperations
    {
        /// <summary>
        /// Gets a live energy reading from the device service, if applicable.
        /// </summary>
        /// <param name="deviceUuid">The device UUID.</param>
        /// <param name="serviceUuid">The service UUID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<EnergyReadingEntity> GetDeviceServiceEnergyAsync(Guid deviceUuid, Guid serviceUuid, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets historical energy data from a device service, if applicable.
        /// </summary>
        /// <param name="deviceUuid">The device UUID.</param>
        /// <param name="serviceUuid">The service UUID.</param>
        /// <param name="from">The date/time to start the search.</param>
        /// <param name="to">The date/time to end the search.</param>
        /// <param name="grouping">The grouping for the resulting data.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<HistoricalEnergyReadingEntity[]> GetDeviceServiceEnergyHistoricalAsync(Guid deviceUuid, Guid serviceUuid, DateTime from, DateTime to, HistoricalGrouping grouping = HistoricalGrouping.Minute, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets historical energy consumption from a device service, if applicable.
        /// </summary>
        /// <param name="deviceUuid">The device UUID.</param>
        /// <param name="serviceUuid">The service UUID.</param>
        /// <param name="from">The date/time to start the search.</param>
        /// <param name="to">The date/time to end the search.</param>
        /// <param name="grouping">The grouping for the resulting data.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<HistoricalEnergyConsumptionEntity[]> GetDeviceServiceEnergyHistoricalConsumptionAsync(Guid deviceUuid, Guid serviceUuid, DateTime from, DateTime to, HistoricalGrouping grouping = HistoricalGrouping.Minute, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the total historical energy consumption from a device service, if applicable.
        /// </summary>
        /// <returns></returns>
        Task<EnergyConsumptionEntity> GetDeviceServiceEnergyConsumptionAsync(Guid deviceUuid, Guid serviceUuid, DateTime from, DateTime to, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Boosts a device by turning it on and off after a certain duration, if applicable.
        /// </summary>
        /// <param name="deviceUuid">The device UUID.</param>
        /// <param name="serviceUuid">The service UUID.</param>
        /// <param name="entity">The request entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The boost timer.</returns>
        Task<TimerEntity> BoostDeviceServiceAsync(Guid deviceUuid, Guid serviceUuid, BoostEntity entity, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Boosts a device by turning it on and off after a certain duration, if applicable.
        /// </summary>
        /// <param name="deviceUuid">The device UUID.</param>
        /// <param name="serviceUuid">The service UUID.</param>
        /// <param name="duration">The duration.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The boost result.</returns>
        Task<TimerEntity> BoostDeviceServiceAsync(Guid deviceUuid, Guid serviceUuid, TimeSpan duration, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Toggles a device service, if applicable.
        /// </summary>
        /// <param name="deviceUuid">The device UUID.</param>
        /// <param name="serviceUuid">The service UUID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The new state.</returns>
        Task<ControlEntity> ToggleDeviceServiceAsync(Guid deviceUuid, Guid serviceUuid, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Controls a device service, if applicable.
        /// </summary>
        /// <param name="deviceUuid">The device UUID.</param>
        /// <param name="serviceUuid">The service UUID.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The new state.</returns>
        Task<ControlEntity> ControlDeviceServiceAsync(Guid deviceUuid, Guid serviceUuid, ControlEntity entity, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Controls a device service, if applicable.
        /// </summary>
        /// <param name="deviceUuid">The device UUID.</param>
        /// <param name="serviceUuid">The service UUID.</param>
        /// <param name="state">The state (on/off).</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The new state.</returns>
        Task<ControlEntity> ControlDeviceServiceAsync(Guid deviceUuid, Guid serviceUuid, bool state, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Edits a device service.
        /// </summary>
        /// <param name="deviceUuid">The device UUID.</param>
        /// <param name="serviceUuid">The service UUID.</param>
        /// <param name="entity">The request entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<DeviceServiceEntity> EditDeviceServiceAsync(Guid deviceUuid, Guid serviceUuid, DeviceServiceEditEntity entity, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Renames a device service.
        /// </summary>
        /// <param name="deviceUuid"></param>
        /// <param name="serviceUuid"></param>
        /// <param name="caption"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<DeviceServiceEntity> RenameDeviceServiceAsync(Guid deviceUuid, Guid serviceUuid, string caption, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets all device services.
        /// </summary>
        /// <param name="deviceUuid">The device UUID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The device services.</returns>
        Task<DeviceServiceEntity[]> ListDeviceServicesAsync(Guid deviceUuid, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets a device service.
        /// </summary>
        /// <param name="deviceUuid">The device UUID.</param>
        /// <param name="serviceUuid">The service UUID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<DeviceServiceEntity> GetDeviceServiceAsync(Guid deviceUuid, Guid serviceUuid, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets all device users.
        /// </summary>
        /// <param name="deviceUuid">The device UUID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<DeviceUserEntity[]> ListDeviceUsersAsync(Guid deviceUuid, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Scans the device user list.
        /// </summary>
        /// <param name="deviceUuid">The device UUID.</param>
        /// <param name="limit">The limit, maximum of 50.</param>
        /// <param name="cursor">The previously returned cursor.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<ScanResult<DeviceUserEntity>> ScanDeviceUsersAsync(Guid deviceUuid, int limit = 50, Cursor cursor = default(Cursor), CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Adds a user to a device.
        /// </summary>
        /// <param name="deviceUuid">The device UUID.</param>
        /// <param name="username">The username.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The user device entity.</returns>
        Task<DeviceUserEntity> AddDeviceUserAsync(Guid deviceUuid, string username, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Adds a user to a device.
        /// </summary>
        /// <param name="deviceUuid">The device UUID.</param>
        /// <param name="entity">The request entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The user device entity.</returns>
        Task<DeviceUserEntity> AddDeviceUserAsync(Guid deviceUuid, DeviceUserAddEntity entity, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Deletes a user from a device.
        /// </summary>
        /// <param name="deviceUuid"></param>
        /// <param name="userUuid"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task DeleteDeviceUserAsync(Guid deviceUuid, Guid userUuid, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets a device by UUID.
        /// </summary>
        /// <param name="deviceUuid">The UUID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<DeviceEntity> GetDeviceAsync(Guid deviceUuid, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Edits a device entity.
        /// </summary>
        /// <param name="deviceUuid">The UUID.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The edited device entity.</returns>
        Task<DeviceEntity> EditDeviceAsync(Guid deviceUuid, DeviceEditEntity entity, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Deletes a device entity.
        /// </summary>
        /// <param name="deviceUuid">The UUID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task DeleteDeviceAsync(Guid deviceUuid, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Renames a device.
        /// </summary>
        /// <returns></returns>
        Task<DeviceEntity> RenameDeviceAsync(Guid deviceUuid, string name, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets all devices.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The devices.</returns>
        Task<DeviceEntity[]> ListDevicesAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Scans the device list.
        /// </summary>
        /// <param name="limit">The limit, maximum of 50.</param>
        /// <param name="cursor">The previously returned cursor.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<ScanResult<DeviceEntity>> ScanDevicesAsync(int limit = 50, Cursor cursor = default(Cursor), CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Scans the device timer list.
        /// </summary>
        /// <param name="deviceUuid">The device UUID.</param>
        /// <param name="limit">The limit, maximum of 50.</param>
        /// <param name="cursor">The previously returned cursor.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<ScanResult<TimerEntity>> ScanDeviceTimersAsync(Guid deviceUuid, int limit = 50, Cursor cursor = default(Cursor), CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets all device timers.
        /// </summary>
        /// <param name="deviceUuid">The device UUID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<TimerEntity[]> ListDeviceTimersAsync(Guid deviceUuid, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets a device timer.
        /// </summary>
        /// <param name="deviceUuid">The device UUID.</param>
        /// <param name="timerUuid">The timer UUID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<TimerEntity> GetDeviceTimerAsync(Guid deviceUuid, Guid timerUuid, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Adds a timer to the device.
        /// </summary>
        /// <param name="deviceUuid">The device UUID.</param>
        /// <param name="entity">The timer entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<TimerEntity> AddDeviceTimerAsync(Guid deviceUuid, DeviceTimerAddEntity entity, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Deletes a timer from the device.
        /// </summary>
        /// <param name="deviceUuid">The device UUID.</param>
        /// <param name="timerUuid">The timer UUID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task DeleteDeviceTimerAsync(Guid deviceUuid, Guid timerUuid, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Scans the device events list.
        /// </summary>
        /// <param name="deviceUuid">The device UUID.</param>
        /// <param name="limit">The limit, maximum of 50.</param>
        /// <param name="cursor">The previously returned cursor.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The event results.</returns>
        Task<ScanResult<EventEntity>> ScanDeviceEventsAsync(Guid deviceUuid, int limit = 50, Cursor cursor = default(Cursor), CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets all device events. This may return alot of results, see <see cref="ScanDeviceEventsAsync"/> instead.
        /// </summary>
        /// <param name="deviceUuid">The device UUID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>All events.</returns>
        Task<EventEntity[]> ListDeviceEventsAsync(Guid deviceUuid, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Creates a device setup token.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <remarks>This operation is internal and won't work with normal API keys. Nor is it stable.</remarks>
        /// <returns>The setup token.</returns>
        Task<DeviceSetupTokenEntity> AddDeviceSetupTokenAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
