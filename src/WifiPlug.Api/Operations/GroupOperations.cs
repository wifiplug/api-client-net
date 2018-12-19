// Copyright (C) WIFIPLUG. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WifiPlug.Api.Entities;

namespace WifiPlug.Api.Operations
{
    /// <summary>
    /// Provides operations for group resources.
    /// </summary>
    public class GroupOperations : IGroupOperations
    {
        /// <summary>
        /// The API client.
        /// </summary>
        protected IBaseApiRequestor _client;

        /// <summary>
        /// Scans the group list.
        /// </summary>
        /// <param name="limit">The limit, maximum of 50.</param>
        /// <param name="cursor">The previously returned cursor.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The scanned groups.</returns>
        public async Task<ScanResult<GroupEntity>> ScanGroupsAsync(int limit = 50, Cursor cursor = default(Cursor), CancellationToken cancellationToken = default(CancellationToken)) {
            if (limit < 0 || limit > 50)
                throw new ArgumentOutOfRangeException(nameof(limit), "The limit cannot be larger than 50");

            // build uri
            string uri = $"group?limit={limit}";

            if (!cursor.IsEnd)
                uri += string.Format("&cursor={0}", WebUtility.UrlEncode(cursor.Token));

            // load results for current cursor
            GroupResultsEntity entity = await _client.RequestJsonSerializedAsync<GroupResultsEntity>(HttpMethod.Get, uri, cancellationToken).ConfigureAwait(false);

            return new ScanResult<GroupEntity>(entity.Groups, entity.TotalGroups, entity.Cursor);
        }

        /// <summary>
        /// Gets all groups.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>An array of all the user's groups.</returns>
        public async Task<GroupEntity[]> ListGroupsAsync(CancellationToken cancellationToken = default(CancellationToken)) {
            ScanResult<GroupEntity> scan = null;
            List<GroupEntity> groups = new List<GroupEntity>();

            while (scan == null || !scan.Cursor.IsEnd) {
                scan = await ScanGroupsAsync(50, scan == null ? default(Cursor) : scan.Cursor, cancellationToken).ConfigureAwait(false);
                groups.AddRange(scan.Entities);
            }

            return groups.ToArray();
        }

        /// <summary>
        /// Adds a group.
        /// </summary>
        /// <param name="entity">The group entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The added group.</returns>
        public Task<GroupEntity> AddGroupAsync(GroupAddEntity entity, CancellationToken cancellationToken = default(CancellationToken)) {
            return _client.RequestJsonSerializedAsync<GroupAddEntity, GroupEntity>(HttpMethod.Post, "group/add", entity, cancellationToken);
        }

        /// <summary>
        /// Gets a group by UUID.
        /// </summary>
        /// <param name="groupUuid">The group UUID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The group.</returns>
        public Task<GroupEntity> GetGroupAsync(Guid groupUuid, CancellationToken cancellationToken = default(CancellationToken)) {
            return _client.RequestJsonSerializedAsync<GroupEntity>(HttpMethod.Get, $"group/{groupUuid}", cancellationToken);
        }

        /// <summary>
        /// Edits a group.
        /// </summary>
        /// <param name="groupUuid">The group UUID.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The edited group.</returns>
        public Task<GroupEntity> EditGroupAsync(Guid groupUuid, GroupEditEntity entity, CancellationToken cancellationToken = default(CancellationToken)) {
            return _client.RequestJsonSerializedAsync<GroupEditEntity, GroupEntity>(HttpMethod.Post, $"group/{groupUuid}", entity, cancellationToken);
        }

        /// <summary>
        /// Renames a group.
        /// </summary>
        /// <param name="groupUuid">The group UUID.</param>
        /// <param name="name">The group name.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The edited group.</returns>
        public Task<GroupEntity> RenameGroupAsync(Guid groupUuid, string name, CancellationToken cancellationToken = default(CancellationToken)) {
            return _client.RequestJsonSerializedAsync<GroupEditEntity, GroupEntity>(HttpMethod.Post, $"group/{groupUuid}", new GroupEditEntity() {
                Name = name
            }, cancellationToken);
        }

        /// <summary>
        /// Deletes a group.
        /// </summary>
        /// <param name="groupUuid">The group UUID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task DeleteGroupAsync(Guid groupUuid, CancellationToken cancellationToken = default(CancellationToken)) {
            return _client.RequestAsync(HttpMethod.Delete, $"group/{groupUuid}", null, cancellationToken);
        }

        /// <summary>
        /// Adds an item to a group.
        /// </summary>
        /// <param name="groupUuid">The group UUID.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The added group item.</returns>
        public Task<GroupItemEntity> AddGroupItemAsync(Guid groupUuid, GroupAddItemEntity entity, CancellationToken cancellationToken = default(CancellationToken)) {
            return _client.RequestJsonSerializedAsync<GroupAddItemEntity, GroupItemEntity>(HttpMethod.Post, $"group/{groupUuid}/item/add", entity, cancellationToken);
        }

        /// <summary>
        /// Gets an item in a group.
        /// </summary>
        /// <param name="groupUuid">The group UUID.</param>
        /// <param name="itemUuid">The item UUID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The group item.</returns>
        public Task<GroupItemEntity> GetGroupItemAsync(Guid groupUuid, Guid itemUuid, CancellationToken cancellationToken = default(CancellationToken)) {
            return _client.RequestJsonSerializedAsync<GroupItemEntity>(HttpMethod.Get, $"group/{groupUuid}/item/{itemUuid}", cancellationToken);
        }

        /// <summary>
        /// Deletes a group item.
        /// </summary>
        /// <param name="groupUuid">The group UUID.</param>
        /// <param name="itemUuid">The item UUID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task DeleteGroupItemAsync(Guid groupUuid, Guid itemUuid, CancellationToken cancellationToken = default(CancellationToken)) {
            return _client.RequestAsync(HttpMethod.Delete, $"group/{groupUuid}/item/{itemUuid}", null, cancellationToken);
        }

        /// <summary>
        /// Controls a group.
        /// </summary>
        /// <param name="groupUuid">The group UUID.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The new state.</returns>
        public Task<ControlEntity> ControlGroupAsync(Guid groupUuid, ControlEntity entity, CancellationToken cancellationToken = default(CancellationToken)) {
            return _client.RequestJsonSerializedAsync<ControlEntity, ControlEntity>(HttpMethod.Post, $"group/{groupUuid}/control", entity, cancellationToken);
        }

        /// <summary>
        /// Controls a group.
        /// </summary>
        /// <param name="groupUuid">The group UUID.</param>
        /// <param name="state">The new state.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The new state.</returns>
        public Task<ControlEntity> ControlGroupAsync(Guid groupUuid, bool state, CancellationToken cancellationToken = default(CancellationToken)) {
            return ControlGroupAsync(groupUuid, new ControlEntity() {
                State = state
            }, cancellationToken);
        }

        /// <summary>
        /// Toggles a group.
        /// </summary>
        /// <param name="groupUuid">The group UUID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The new state.</returns>
        [Obsolete("Group toggling, use ControlGroupAsync")]
        public Task<ControlEntity> ToggleGroupAsync(Guid groupUuid, CancellationToken cancellationToken = default(CancellationToken)) {
            return _client.RequestJsonSerializedAsync<ControlEntity>(HttpMethod.Post, $"group/{groupUuid}/toggle", cancellationToken);
        }

        /// <summary>
        /// Boosts a group by turning it on and off after a certain duration, if applicable.
        /// </summary>
        /// <param name="groupUuid">The group UUID.</param>
        /// <param name="entity">The request entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The boost timer.</returns>
        public Task<TimerEntity> BoostGroupAsync(Guid groupUuid, BoostEntity entity, CancellationToken cancellationToken = default(CancellationToken)) {
            return _client.RequestJsonSerializedAsync<BoostEntity, TimerEntity>(HttpMethod.Post, $"group/{groupUuid}/boost", entity, cancellationToken);
        }

        /// <summary>
        /// Boosts a group by turning it on and off after a certain duration, if applicable.
        /// </summary>
        /// <param name="groupUuid">The group UUID.</param>
        /// <param name="duration">The duration.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The boost result.</returns>
        public Task<TimerEntity> BoostGroupAsync(Guid groupUuid, TimeSpan duration, CancellationToken cancellationToken = default(CancellationToken)) {
            return BoostGroupAsync(groupUuid, new BoostEntity() {
                Duration = (int)duration.TotalSeconds
            }, cancellationToken);
        }

        /// <summary>
        /// Scans the group timer list.
        /// </summary>
        /// <param name="groupUuid">The group UUID.</param>
        /// <param name="limit">The limit, maximum of 50.</param>
        /// <param name="cursor">The previously returned cursor.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<ScanResult<TimerEntity>> ScanGroupTimersAsync(Guid groupUuid, int limit = 50, Cursor cursor = default(Cursor), CancellationToken cancellationToken = default(CancellationToken)) {
            if (limit < 0 || limit > 50)
                throw new ArgumentOutOfRangeException(nameof(limit), "The limit cannot be larger than 50");

            // build uri
            string uri = $"group/{groupUuid}/timer?limit={limit}";

            if (!cursor.IsEnd)
                uri += string.Format("&cursor={0}", WebUtility.UrlEncode(cursor.Token));

            // load results for current cursor
            TimerResultsEntity entity = await _client.RequestJsonSerializedAsync<TimerResultsEntity>(HttpMethod.Get, uri, cancellationToken).ConfigureAwait(false);

            return new ScanResult<TimerEntity>(entity.Timers, entity.TotalTimers, entity.Cursor);
        }

        /// <summary>
        /// Gets all group timers.
        /// </summary>
        /// <param name="groupUuid">The group UUID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<TimerEntity[]> ListGroupTimersAsync(Guid groupUuid, CancellationToken cancellationToken = default(CancellationToken)) {
            ScanResult<TimerEntity> scan = null;
            List<TimerEntity> timers = new List<TimerEntity>();

            while (scan == null || !scan.Cursor.IsEnd) {
                scan = await ScanGroupTimersAsync(groupUuid, 1, scan == null ? default(Cursor) : scan.Cursor, cancellationToken).ConfigureAwait(false);
                timers.AddRange(scan.Entities);
            }

            return timers.ToArray();
        }
        
        /// <summary>
        /// Adds a timer to the group.
        /// </summary>
        /// <param name="groupUuid">The group UUID.</param>
        /// <param name="entity">The timer entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<TimerEntity> AddGroupTimerAsync(Guid groupUuid, GroupTimerAddEntity entity, CancellationToken cancellationToken = default(CancellationToken)) {
            return _client.RequestJsonSerializedAsync<GroupTimerAddEntity, TimerEntity>(HttpMethod.Post, $"group/{groupUuid}/timer/add", entity, cancellationToken);
        }

        /// <summary>
        /// Deletes a timer from the group.
        /// </summary>
        /// <param name="groupUuid">The group UUID.</param>
        /// <param name="timerUuid">The timer UUID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task DeleteGroupTimerAsync(Guid groupUuid, Guid timerUuid, CancellationToken cancellationToken = default(CancellationToken)) {
            return _client.RequestAsync(HttpMethod.Delete, $"group/{groupUuid}/timer/{timerUuid}", null, cancellationToken);
        }

        /// <summary>
        /// Gets a group timer.
        /// </summary>
        /// <param name="groupUuid">The group UUID.</param>
        /// <param name="timerUuid">The timer UUID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<TimerEntity> GetGroupTimerAsync(Guid groupUuid, Guid timerUuid, CancellationToken cancellationToken = default(CancellationToken)) {
            return _client.RequestJsonSerializedAsync<TimerEntity>(HttpMethod.Get, $"group/{groupUuid}/timer/{timerUuid}", cancellationToken);
        }

        /// <summary>
        /// Scans the group item list.
        /// </summary>
        /// <param name="groupUuid">The group UUID.</param>
        /// <param name="limit">The limit, maximum of 50.</param>
        /// <param name="cursor">The previously returned cursor.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<ScanResult<GroupItemEntity>> ScanGroupItemsAsync(Guid groupUuid, int limit = 50, Cursor cursor = default(Cursor), CancellationToken cancellationToken = default(CancellationToken)) {
            if (limit < 0 || limit > 50)
                throw new ArgumentOutOfRangeException(nameof(limit), "The limit cannot be larger than 50");

            // build uri
            string uri = $"group/{groupUuid}/item?limit={limit}";

            if (!cursor.IsEnd)
                uri += string.Format("&cursor={0}", WebUtility.UrlEncode(cursor.Token));

            // load results for current cursor
            GroupItemResultsEntity entity = await _client.RequestJsonSerializedAsync<GroupItemResultsEntity>(HttpMethod.Get, uri, cancellationToken).ConfigureAwait(false);

            return new ScanResult<GroupItemEntity>(entity.Items, entity.TotalItems, entity.Cursor);
        }

        /// <summary>
        /// Gets all group items.
        /// </summary>
        /// <param name="groupUuid">The group UUID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<GroupItemEntity[]> ListGroupItemsAsync(Guid groupUuid, CancellationToken cancellationToken = default(CancellationToken)) {
            ScanResult<GroupItemEntity> scan = null;
            List<GroupItemEntity> items = new List<GroupItemEntity>();

            while (scan == null || !scan.Cursor.IsEnd) {
                scan = await ScanGroupItemsAsync(groupUuid, 1, scan == null ? default(Cursor) : scan.Cursor, cancellationToken).ConfigureAwait(false);
                items.AddRange(scan.Entities);
            }

            return items.ToArray();
        }

        /// <summary>
        /// Creates a group operations object.
        /// </summary>
        /// <param name="client">The client.</param>
        protected internal GroupOperations(IBaseApiRequestor client) {
            _client = client;
        }
    }
}
