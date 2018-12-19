// Copyright (C) WIFIPLUG. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WifiPlug.Api.Entities;
using WifiPlug.Api.Schema;

namespace WifiPlug.Api.Operations
{
    /// <summary>
    /// Provides operations for device resources.
    /// </summary>
    public class DeviceOperations : IDeviceOperations
    {
        /// <summary>
        /// The API client.
        /// </summary>
        protected IBaseApiRequestor _client;
        
        /// <summary>
        /// Gets a live energy reading from the device service, if applicable.
        /// </summary>
        /// <param name="deviceUuid">The device UUID.</param>
        /// <param name="serviceUuid">The service UUID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<EnergyReadingEntity> GetDeviceServiceEnergyAsync(Guid deviceUuid, Guid serviceUuid, CancellationToken cancellationToken = default(CancellationToken)) {
            return _client.RequestJsonSerializedAsync<EnergyReadingEntity>(HttpMethod.Get, $"device/{deviceUuid}/service/{serviceUuid}/energy", cancellationToken);
        }

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
        public Task<HistoricalEnergyReadingEntity[]> GetDeviceServiceEnergyHistoricalAsync(Guid deviceUuid, Guid serviceUuid, DateTime from, DateTime to, HistoricalGrouping grouping = HistoricalGrouping.Minute, CancellationToken cancellationToken = default(CancellationToken)) {
            // convert to grouping string
            string groupingStr = "minute";

            if (grouping == HistoricalGrouping.Hour)
                groupingStr = "hour";
            else if (grouping == HistoricalGrouping.Day)
                groupingStr = "day";
            
            return _client.RequestJsonSerializedAsync<HistoricalEnergyReadingEntity[]>(HttpMethod.Get, 
                $"device/{deviceUuid}/service/{serviceUuid}/energy/historic?grouping={groupingStr}&date_from={from.ToString("yyyy-MM-ddTHH:mm:ssZ")}&date_to={to.ToString("yyyy-MM-ddTHH:mm:ssZ")}");
        }

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
        public Task<HistoricalEnergyConsumptionEntity[]> GetDeviceServiceEnergyHistoricalConsumptionAsync(Guid deviceUuid, Guid serviceUuid, DateTime from, DateTime to, HistoricalGrouping grouping = HistoricalGrouping.Minute, CancellationToken cancellationToken = default(CancellationToken)) {
            // convert to grouping string
            string groupingStr = "minute";

            if (grouping == HistoricalGrouping.Hour)
                groupingStr = "hour";
            else if (grouping == HistoricalGrouping.Day)
                groupingStr = "day";

            return _client.RequestJsonSerializedAsync<HistoricalEnergyConsumptionEntity[]>(HttpMethod.Get,
                $"device/{deviceUuid}/service/{serviceUuid}/energy/consumption/historic?grouping={groupingStr}&date_from={from.ToString("yyyy-MM-ddTHH:mm:ssZ")}&date_to={to.ToString("yyyy-MM-ddTHH:mm:ssZ")}");
        }

        /// <summary>
        /// Gets the total historical energy consumption from a device service, if applicable.
        /// </summary>
        /// <returns></returns>
        public Task<EnergyConsumptionEntity> GetDeviceServiceEnergyConsumptionAsync(Guid deviceUuid, Guid serviceUuid, DateTime from, DateTime to, CancellationToken cancellationToken = default(CancellationToken)) {
            return _client.RequestJsonSerializedAsync<EnergyConsumptionEntity>(HttpMethod.Get,
                $"device/{deviceUuid}/service/{serviceUuid}/energy/consumption?date_from={from.ToString("yyyy-MM-ddTHH:mm:ssZ")}&date_to={to.ToString("yyyy-MM-ddTHH:mm:ssZ")}");
        }

        /// <summary>
        /// Boosts a device by turning it on and off after a certain duration, if applicable.
        /// </summary>
        /// <param name="deviceUuid">The device UUID.</param>
        /// <param name="serviceUuid">The service UUID.</param>
        /// <param name="entity">The request entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The boost timer.</returns>
        public Task<TimerEntity> BoostDeviceServiceAsync(Guid deviceUuid, Guid serviceUuid, BoostEntity entity, CancellationToken cancellationToken = default(CancellationToken)) {
            return _client.RequestJsonSerializedAsync<BoostEntity, TimerEntity>(HttpMethod.Post, $"device/{deviceUuid}/service/{serviceUuid}/boost", entity, cancellationToken);
        }

        /// <summary>
        /// Boosts a device by turning it on and off after a certain duration, if applicable.
        /// </summary>
        /// <param name="deviceUuid">The device UUID.</param>
        /// <param name="serviceUuid">The service UUID.</param>
        /// <param name="duration">The duration.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The boost result.</returns>
        public Task<TimerEntity> BoostDeviceServiceAsync(Guid deviceUuid, Guid serviceUuid, TimeSpan duration, CancellationToken cancellationToken = default(CancellationToken)) {
            return BoostDeviceServiceAsync(deviceUuid, serviceUuid, new BoostEntity() {
                Duration = (int)duration.TotalSeconds
            }, cancellationToken);
        }

        /// <summary>
        /// Toggles a device service, if applicable.
        /// </summary>
        /// <param name="deviceUuid">The device UUID.</param>
        /// <param name="serviceUuid">The service UUID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The new state.</returns>
        public Task<ControlEntity> ToggleDeviceServiceAsync(Guid deviceUuid, Guid serviceUuid, CancellationToken cancellationToken = default(CancellationToken)) {
            return _client.RequestJsonSerializedAsync<ControlEntity>(HttpMethod.Post, $"device/{deviceUuid}/service/{serviceUuid}/toggle", cancellationToken);
        }

        /// <summary>
        /// Controls a device service, if applicable.
        /// </summary>
        /// <param name="deviceUuid">The device UUID.</param>
        /// <param name="serviceUuid">The service UUID.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The new state.</returns>
        public Task<ControlEntity> ControlDeviceServiceAsync(Guid deviceUuid, Guid serviceUuid, ControlEntity entity, CancellationToken cancellationToken = default(CancellationToken)) {
            return _client.RequestJsonSerializedAsync<ControlEntity, ControlEntity>(HttpMethod.Post, $"device/{deviceUuid}/service/{serviceUuid}/control", entity, cancellationToken);
        }

        /// <summary>
        /// Controls a device service, if applicable.
        /// </summary>
        /// <param name="deviceUuid">The device UUID.</param>
        /// <param name="serviceUuid">The service UUID.</param>
        /// <param name="state">The state (on/off).</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The new state.</returns>
        public Task<ControlEntity> ControlDeviceServiceAsync(Guid deviceUuid, Guid serviceUuid, bool state, CancellationToken cancellationToken = default(CancellationToken)) {
            return ControlDeviceServiceAsync(deviceUuid, serviceUuid, new ControlEntity() {
                State = state
            }, cancellationToken);
        }

        /// <summary>
        /// Edits a device service.
        /// </summary>
        /// <param name="deviceUuid">The device UUID.</param>
        /// <param name="serviceUuid">The service UUID.</param>
        /// <param name="entity">The request entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<DeviceServiceEntity> EditDeviceServiceAsync(Guid deviceUuid, Guid serviceUuid, DeviceServiceEditEntity entity, CancellationToken cancellationToken = default(CancellationToken)) {
            return _client.RequestJsonSerializedAsync<DeviceServiceEditEntity, DeviceServiceEntity>(HttpMethod.Post, $"device/{deviceUuid}/service/{serviceUuid}", entity, cancellationToken);
        }

        /// <summary>
        /// Renames a device service.
        /// </summary>
        /// <param name="deviceUuid"></param>
        /// <param name="serviceUuid"></param>
        /// <param name="caption"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<DeviceServiceEntity> RenameDeviceServiceAsync(Guid deviceUuid, Guid serviceUuid, string caption, CancellationToken cancellationToken = default(CancellationToken)) {
            return EditDeviceServiceAsync(deviceUuid, serviceUuid, new DeviceServiceEditEntity() {
                Caption = caption
            }, cancellationToken);
        }

        /// <summary>
        /// Gets all device services.
        /// </summary>
        /// <param name="deviceUuid">The device UUID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The device services.</returns>
        public Task<DeviceServiceEntity[]> ListDeviceServicesAsync(Guid deviceUuid, CancellationToken cancellationToken = default(CancellationToken)) {
            return _client.RequestJsonSerializedAsync<DeviceServiceEntity[]>(HttpMethod.Get, $"device/{deviceUuid}/service", cancellationToken);
        }

        /// <summary>
        /// Gets a device service.
        /// </summary>
        /// <param name="deviceUuid">The device UUID.</param>
        /// <param name="serviceUuid">The service UUID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<DeviceServiceEntity> GetDeviceServiceAsync(Guid deviceUuid, Guid serviceUuid, CancellationToken cancellationToken = default(CancellationToken)) {
            return _client.RequestJsonSerializedAsync<DeviceServiceEntity>(HttpMethod.Get, $"device/{deviceUuid}/service/{serviceUuid}", cancellationToken);
        }

        /// <summary>
        /// Gets all device users.
        /// </summary>
        /// <param name="deviceUuid">The device UUID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<DeviceUserEntity[]> ListDeviceUsersAsync(Guid deviceUuid, CancellationToken cancellationToken = default(CancellationToken)) {
            ScanResult<DeviceUserEntity> scan = null;
            List<DeviceUserEntity> users = new List<DeviceUserEntity>();

            while (scan == null || !scan.Cursor.IsEnd) {
                scan = await ScanDeviceUsersAsync(deviceUuid, 50, scan == null ? default(Cursor) : scan.Cursor, cancellationToken).ConfigureAwait(false);
                users.AddRange(scan.Entities);
            }

            return users.ToArray();
        }

        /// <summary>
        /// Scans the device user list.
        /// </summary>
        /// <param name="deviceUuid">The device UUID.</param>
        /// <param name="limit">The limit, maximum of 50.</param>
        /// <param name="cursor">The previously returned cursor.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<ScanResult<DeviceUserEntity>> ScanDeviceUsersAsync(Guid deviceUuid, int limit = 50, Cursor cursor = default(Cursor), CancellationToken cancellationToken = default(CancellationToken)) {
            if (limit < 0 || limit > 50)
                throw new ArgumentOutOfRangeException(nameof(limit), "The limit cannot be larger than 50");

            // build uri
            string uri = $"device/{deviceUuid}/user?limit={limit}";

            if (!cursor.IsEnd)
                uri += string.Format("&cursor={0}", WebUtility.UrlEncode(cursor.Token));

            // load results for current cursor
            DeviceUserResultsEntity entity = await _client.RequestJsonSerializedAsync<DeviceUserResultsEntity>(HttpMethod.Get, uri, cancellationToken).ConfigureAwait(false);

            return new ScanResult<DeviceUserEntity>(entity.Users, entity.TotalUsers, entity.Cursor);
        }

        /// <summary>
        /// Adds a user to a device.
        /// </summary>
        /// <param name="deviceUuid">The device UUID.</param>
        /// <param name="username">The username.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The user device entity.</returns>
        public Task<DeviceUserEntity> AddDeviceUserAsync(Guid deviceUuid, string username, CancellationToken cancellationToken = default(CancellationToken)) {
            return AddDeviceUserAsync(deviceUuid, new DeviceUserAddEntity() {
                Username = username
            }, cancellationToken);
        }

        /// <summary>
        /// Adds a user to a device.
        /// </summary>
        /// <param name="deviceUuid">The device UUID.</param>
        /// <param name="entity">The request entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The user device entity.</returns>
        public Task<DeviceUserEntity> AddDeviceUserAsync(Guid deviceUuid, DeviceUserAddEntity entity, CancellationToken cancellationToken = default(CancellationToken)) {
            return _client.RequestJsonSerializedAsync<DeviceUserAddEntity, DeviceUserEntity>(HttpMethod.Post, $"device/{deviceUuid}/user/add", entity, default(CancellationToken));
        }

        /// <summary>
        /// Deletes a user from a device.
        /// </summary>
        /// <param name="deviceUuid"></param>
        /// <param name="userUuid"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task DeleteDeviceUserAsync(Guid deviceUuid, Guid userUuid, CancellationToken cancellationToken = default(CancellationToken)) {
            return _client.RequestAsync(HttpMethod.Delete, $"device/{deviceUuid}/user/{userUuid}", null, cancellationToken);
        }

        /// <summary>
        /// Gets a device by UUID.
        /// </summary>
        /// <param name="deviceUuid">The UUID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<DeviceEntity> GetDeviceAsync(Guid deviceUuid, CancellationToken cancellationToken = default(CancellationToken)) {
            return _client.RequestJsonSerializedAsync<DeviceEntity>(HttpMethod.Get, $"device/{deviceUuid}", cancellationToken);
        }

        /// <summary>
        /// Edits a device entity.
        /// </summary>
        /// <param name="deviceUuid">The UUID.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The edited device entity.</returns>
        public Task<DeviceEntity> EditDeviceAsync(Guid deviceUuid, DeviceEditEntity entity, CancellationToken cancellationToken = default(CancellationToken)) {
            return _client.RequestJsonSerializedAsync<DeviceEditEntity, DeviceEntity>(HttpMethod.Post, $"device/{deviceUuid}", entity, cancellationToken);
        }

        /// <summary>
        /// Deletes a device entity.
        /// </summary>
        /// <param name="deviceUuid">The UUID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task DeleteDeviceAsync(Guid deviceUuid, CancellationToken cancellationToken = default(CancellationToken)) {
            return _client.RequestAsync(HttpMethod.Delete, $"device/{deviceUuid}", null, cancellationToken);
        }

        /// <summary>
        /// Renames a device.
        /// </summary>
        /// <returns></returns>
        public Task<DeviceEntity> RenameDeviceAsync(Guid deviceUuid, string name, CancellationToken cancellationToken = default(CancellationToken)) {
            return EditDeviceAsync(deviceUuid, new DeviceEditEntity() {
                Name = name
            }, cancellationToken);
        }

        /// <summary>
        /// Gets all devices.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The devices.</returns>
        public async Task<DeviceEntity[]> ListDevicesAsync(CancellationToken cancellationToken = default(CancellationToken)) {
            ScanResult<DeviceEntity> scan = null;
            List<DeviceEntity> devices = new List<DeviceEntity>();

            while (scan == null || !scan.Cursor.IsEnd) {
                scan = await ScanDevicesAsync(50, scan == null ? default(Cursor) : scan.Cursor, cancellationToken).ConfigureAwait(false);
                devices.AddRange(scan.Entities);
            }

            return devices.ToArray();
        }

        /// <summary>
        /// Scans the device list.
        /// </summary>
        /// <param name="limit">The limit, maximum of 50.</param>
        /// <param name="cursor">The previously returned cursor.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<ScanResult<DeviceEntity>> ScanDevicesAsync(int limit = 50, Cursor cursor = default(Cursor), CancellationToken cancellationToken = default(CancellationToken)) {
            if (limit < 0 || limit > 50)
                throw new ArgumentOutOfRangeException(nameof(limit), "The limit cannot be larger than 50");

            // build uri
            string uri = $"device?limit={limit}";

            if (!cursor.IsEnd)
                uri += string.Format("&cursor={0}", WebUtility.UrlEncode(cursor.Token));

            // load results for current cursor
            DeviceResultsEntity entity = await _client.RequestJsonSerializedAsync<DeviceResultsEntity>(HttpMethod.Get, uri, cancellationToken).ConfigureAwait(false);

            return new ScanResult<DeviceEntity>(entity.Devices, entity.TotalDevices, entity.Cursor);
        }

        /// <summary>
        /// Scans the device timer list.
        /// </summary>
        /// <param name="deviceUuid">The device UUID.</param>
        /// <param name="limit">The limit, maximum of 50.</param>
        /// <param name="cursor">The previously returned cursor.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<ScanResult<TimerEntity>> ScanDeviceTimersAsync(Guid deviceUuid, int limit = 50, Cursor cursor = default(Cursor), CancellationToken cancellationToken = default(CancellationToken)) {
            if (limit < 0 || limit > 50)
                throw new ArgumentOutOfRangeException(nameof(limit), "The limit cannot be larger than 50");

            // build uri
            string uri = $"device/{deviceUuid}/timer?limit={limit}";

            if (!cursor.IsEnd)
                uri += string.Format("&cursor={0}", WebUtility.UrlEncode(cursor.Token));

            // load results for current cursor
            TimerResultsEntity entity = await _client.RequestJsonSerializedAsync<TimerResultsEntity>(HttpMethod.Get, uri, cancellationToken).ConfigureAwait(false);

            return new ScanResult<TimerEntity>(entity.Timers, entity.TotalTimers, entity.Cursor);
        }

        /// <summary>
        /// Gets all device timers.
        /// </summary>
        /// <param name="deviceUuid">The device UUID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<TimerEntity[]> ListDeviceTimersAsync(Guid deviceUuid, CancellationToken cancellationToken = default(CancellationToken)) {
            ScanResult<TimerEntity> scan = null;
            List<TimerEntity> timers = new List<TimerEntity>();

            while (scan == null || !scan.Cursor.IsEnd) {
                scan = await ScanDeviceTimersAsync(deviceUuid, 50, scan == null ? default(Cursor) : scan.Cursor, cancellationToken).ConfigureAwait(false);
                timers.AddRange(scan.Entities);
            }

            return timers.ToArray();
        }

        /// <summary>
        /// Gets a device timer.
        /// </summary>
        /// <param name="deviceUuid">The device UUID.</param>
        /// <param name="timerUuid">The timer UUID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<TimerEntity> GetDeviceTimerAsync(Guid deviceUuid, Guid timerUuid, CancellationToken cancellationToken = default(CancellationToken)) {
            return _client.RequestJsonSerializedAsync<TimerEntity>(HttpMethod.Get, $"device/{deviceUuid}/timer/{timerUuid}", cancellationToken);
        }

        /// <summary>
        /// Adds a timer to the device.
        /// </summary>
        /// <param name="deviceUuid">The device UUID.</param>
        /// <param name="entity">The timer entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<TimerEntity> AddDeviceTimerAsync(Guid deviceUuid, DeviceTimerAddEntity entity, CancellationToken cancellationToken = default(CancellationToken)) {
            return _client.RequestJsonSerializedAsync<DeviceTimerAddEntity, TimerEntity>(HttpMethod.Post, $"device/{deviceUuid}/timer/add", entity, cancellationToken);
        }

        /// <summary>
        /// Deletes a timer from the device.
        /// </summary>
        /// <param name="deviceUuid">The device UUID.</param>
        /// <param name="timerUuid">The timer UUID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task DeleteDeviceTimerAsync(Guid deviceUuid, Guid timerUuid, CancellationToken cancellationToken = default(CancellationToken)) {
            return _client.RequestAsync(HttpMethod.Delete, $"device/{deviceUuid}/timer/{timerUuid}", null, cancellationToken);
        }

        /// <summary>
        /// Scans the device events list.
        /// </summary>
        /// <param name="deviceUuid">The device UUID.</param>
        /// <param name="limit">The limit, maximum of 50.</param>
        /// <param name="cursor">The previously returned cursor.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The event results.</returns>
        public async Task<ScanResult<EventEntity>> ScanDeviceEventsAsync(Guid deviceUuid, int limit = 50, Cursor cursor = default(Cursor), CancellationToken cancellationToken = default(CancellationToken)) {
            if (limit < 0 || limit > 50)
                throw new ArgumentOutOfRangeException(nameof(limit), "The limit cannot be larger than 50");

            // build uri
            string uri = $"device/{deviceUuid}/event?limit={limit}";

            if (!cursor.IsEnd)
                uri += string.Format("&cursor={0}", WebUtility.UrlEncode(cursor.Token));

            // load results for current cursor
            EventResultsEntity entity = await _client.RequestJsonSerializedAsync<EventResultsEntity>(HttpMethod.Get, uri, cancellationToken).ConfigureAwait(false);

            return new ScanResult<EventEntity>(entity.Events, entity.TotalEvents, entity.Cursor);
        }

        /// <summary>
        /// Gets all device events. This may return alot of results, see <see cref="ScanDeviceEventsAsync"/> instead.
        /// </summary>
        /// <param name="deviceUuid">The device UUID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>All events.</returns>
        public async Task<EventEntity[]> ListDeviceEventsAsync(Guid deviceUuid, CancellationToken cancellationToken = default(CancellationToken)) {
            ScanResult<EventEntity> scan = null;
            List<EventEntity> events = new List<EventEntity>();

            while (scan == null || !scan.Cursor.IsEnd) {
                scan = await ScanDeviceEventsAsync(deviceUuid, 50, scan == null ? default(Cursor) : scan.Cursor, cancellationToken).ConfigureAwait(false);
                events.AddRange(scan.Entities);
            }

            return events.ToArray();
        }

        /// <summary>
        /// Creates a device setup token.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <remarks>This operation is internal and won't work with normal API keys. Nor is it stable.</remarks>
        /// <returns>The setup token.</returns>
        public Task<DeviceSetupTokenEntity> AddDeviceSetupTokenAsync(CancellationToken cancellationToken = default(CancellationToken)) {
            return _client.RequestJsonSerializedAsync<DeviceSetupTokenEntity>(HttpMethod.Post, $"device/setup_token/add", cancellationToken);
        }

        /// <summary>
        /// Creates a device operations object.
        /// </summary>
        /// <param name="client">The client.</param>
        protected internal DeviceOperations(IBaseApiRequestor client) {
            _client = client;
        }
    }
}
