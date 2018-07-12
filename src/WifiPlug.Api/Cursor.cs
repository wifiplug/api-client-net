using System;
using System.Collections.Generic;
using System.Text;

namespace WifiPlug.Api
{
    /// <summary>
    /// Represents a scanning cursor.
    /// </summary>
    public struct Cursor
    {
        #region Fields
        /// <summary>
        /// An empty cursor representing the end of a scan.
        /// </summary>
        public static readonly Cursor None = default(Cursor);
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the cursor token.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Gets if the cursor represents the end of a scan.
        /// </summary>
        public bool IsEnd {
            get {
                return Token == null;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets the string representation 
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return Token ?? "[End]";
        }
        #endregion

        internal Cursor(string token) {
            Token = token;
        }
    }
}
