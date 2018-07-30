﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WifiPlug.Api
{
    /// <summary>
    /// Represents the result of a scan operation.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public sealed class ScanResult<TEntity>
    {
        #region Fields
        private int _total;
        private Cursor _cursor;
        private IEnumerable<TEntity> _entities;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the next cursor.
        /// </summary>
        public Cursor Cursor {
            get {
                return _cursor;
            }
        }

        /// <summary>
        /// Gets if this result is the end of the scan.
        /// </summary>
        public bool IsEnd {
            get {
                return _cursor.IsEnd;
            }
        }

        /// <summary>
        /// Gets the 
        /// </summary>
        public IEnumerable<TEntity> Entities {
            get {
                return _entities;
            }
        }

        /// <summary>
        /// Gets the total number of entities available according to this scan.
        /// </summary>
        public int Total {
            get {
                return _total;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new scan result without a cursor.
        /// </summary>
        /// <param name="entities">The entities</param>
        /// <param name="total">The total number of entities.</param>
        internal ScanResult(IEnumerable<TEntity> entities, int total) {
            _entities = entities;
            _cursor = Cursor.None;
            _total = total;
        }

        /// <summary>
        /// Creates a new scan result with a cursor.
        /// </summary>
        /// <param name="entities">The entities</param>
        /// <param name="total">The total number of entities.</param>
        /// <param name="nextCursor">The cursor.</param>
        internal ScanResult(IEnumerable<TEntity> entities, int total, Cursor nextCursor) {
            _entities = entities;
            _cursor = nextCursor;
            _total = total;
        }

        /// <summary>
        /// Creates a new scan result with a cursor.
        /// </summary>
        /// <param name="entities">The entities</param>
        /// <param name="total">The total number of entities.</param>
        /// <param name="nextCursor">The cursor.</param>
        internal ScanResult(IEnumerable<TEntity> entities, int total, string nextCursor) {
            _entities = entities;
            _cursor = nextCursor == null ? default(Cursor) : new Cursor(nextCursor);
            _total = total;
        }
        #endregion
    }
}
