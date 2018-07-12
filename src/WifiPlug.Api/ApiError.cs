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

        /// <summary>
        /// Gets any additional data in the error.
        /// </summary>
        public IDictionary<string, object> Data { get; private set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new API error.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="message">The message.</param>
        /// <param name="data">The data.</param>
        internal ApiError(string code, string message, IDictionary<string, object> data) {
            Code = code;
            Message = message;
            Data = data ?? new Dictionary<string, object>();
        }
        #endregion
    }
}
