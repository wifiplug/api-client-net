using System;
using System.Collections.Generic;
using System.Text;

namespace WifiPlug.Api
{
    /// <summary>
    /// Represents a single API error.
    /// </summary>
    public class ApiError
    {
        #region Properties
        /// <summary>
        /// Gets the code.
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        public string Message { get; private set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new API error.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="message">The message.</param>
        internal ApiError(string code, string message) {
            Code = code;
            Message = message;
        }
        #endregion
    }
}
