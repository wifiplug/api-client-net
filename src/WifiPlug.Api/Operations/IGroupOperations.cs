using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WifiPlug.Api.Entities;

namespace WifiPlug.Api.Operations
{
    /// <summary>
    /// Defines an interface for performing group operations.
    /// </summary>
    public interface IGroupOperations
    {
        /// <summary>
        /// Scans the group list.
        /// </summary>
        /// <param name="limit">The limit, maximum of 50.</param>
        /// <param name="cursor">The previously returned cursor.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The scanned groups.</returns>
        Task<ScanResult<GroupEntity>> ScanGroupsAsync(int limit = 50, Cursor cursor = default(Cursor), CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets all groups.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>An array of all the user's groups.</returns>
        Task<GroupEntity[]> ListGroupsAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Adds a group.
        /// </summary>
        /// <param name="entity">The group entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The added group.</returns>
        Task<GroupEntity> AddGroupAsync(GroupAddEntity entity, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets a group by UUID.
        /// </summary>
        /// <param name="groupUuid">The group UUID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The group.</returns>
        Task<GroupEntity> GetGroupAsync(Guid groupUuid, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Edits a group.
        /// </summary>
        /// <param name="groupUuid">The group UUID.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The edited group.</returns>
        Task<GroupEntity> EditGroupAsync(Guid groupUuid, GroupEditEntity entity, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Renames a group.
        /// </summary>
        /// <param name="groupUuid">The group UUID.</param>
        /// <param name="name">The group name.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The edited group.</returns>
        Task<GroupEntity> RenameGroupAsync(Guid groupUuid, string name, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Deletes a group.
        /// </summary>
        /// <param name="groupUuid">The group UUID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task DeleteGroupAsync(Guid groupUuid, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Adds an item to a group.
        /// </summary>
        /// <param name="groupUuid">The group UUID.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The added group item.</returns>
        Task<GroupItemEntity> AddGroupItemAsync(Guid groupUuid, GroupAddItemEntity entity, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets an item in a group.
        /// </summary>
        /// <param name="groupUuid">The group UUID.</param>
        /// <param name="itemUuid">The item UUID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The group item.</returns>
        Task<GroupItemEntity> GetGroupItemAsync(Guid groupUuid, Guid itemUuid, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Deletes a group item.
        /// </summary>
        /// <param name="groupUuid">The group UUID.</param>
        /// <param name="itemUuid">The item UUID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task DeleteGroupItemAsync(Guid groupUuid, Guid itemUuid, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Controls a group.
        /// </summary>
        /// <param name="groupUuid">The group UUID.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The new state.</returns>
        Task<ControlEntity> ControlGroupAsync(Guid groupUuid, ControlEntity entity, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Controls a group.
        /// </summary>
        /// <param name="groupUuid">The group UUID.</param>
        /// <param name="state">The new state.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The new state.</returns>
        Task<ControlEntity> ControlGroupAsync(Guid groupUuid, bool state, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Toggles a group.
        /// </summary>
        /// <param name="groupUuid">The group UUID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The new state.</returns>
        Task<ControlEntity> ToggleGroupAsync(Guid groupUuid, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Boosts a group by turning it on and off after a certain duration, if applicable.
        /// </summary>
        /// <param name="groupUuid">The group UUID.</param>
        /// <param name="entity">The request entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The boost timer.</returns>
        Task<TimerEntity> BoostGroupAsync(Guid groupUuid, BoostEntity entity, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Boosts a group by turning it on and off after a certain duration, if applicable.
        /// </summary>
        /// <param name="groupUuid">The group UUID.</param>
        /// <param name="duration">The duration.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The boost result.</returns>
        Task<TimerEntity> BoostGroupAsync(Guid groupUuid, TimeSpan duration, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Scans the group timer list.
        /// </summary>
        /// <param name="groupUuid">The group UUID.</param>
        /// <param name="limit">The limit, maximum of 50.</param>
        /// <param name="cursor">The previously returned cursor.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<ScanResult<TimerEntity>> ScanGroupTimersAsync(Guid groupUuid, int limit = 50, Cursor cursor = default(Cursor), CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets all group timers.
        /// </summary>
        /// <param name="groupUuid">The group UUID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<TimerEntity[]> ListGroupTimersAsync(Guid groupUuid, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Adds a timer to the group.
        /// </summary>
        /// <param name="groupUuid">The group UUID.</param>
        /// <param name="entity">The timer entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<TimerEntity> AddGroupTimerAsync(Guid groupUuid, GroupTimerAddEntity entity, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Deletes a timer from the group.
        /// </summary>
        /// <param name="groupUuid">The group UUID.</param>
        /// <param name="timerUuid">The timer UUID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task DeleteGroupTimerAsync(Guid groupUuid, Guid timerUuid, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets a group timer.
        /// </summary>
        /// <param name="groupUuid">The group UUID.</param>
        /// <param name="timerUuid">The timer UUID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<TimerEntity> GetGroupTimerAsync(Guid groupUuid, Guid timerUuid, CancellationToken cancellationToken = default(CancellationToken));
    }
}
