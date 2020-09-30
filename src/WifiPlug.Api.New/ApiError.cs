using Newtonsoft.Json;

namespace WifiPlug.Api.New
{
    public sealed class ApiError
    {
        #region Properties
        /// <summary>
        /// Gets the error code.
        /// </summary>
        [JsonProperty("error")]
        public string Code { get; }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; }
        #endregion

        #region Constructors
        public ApiError(string code, string message)
            => (Code, Message) = (code, message);
        #endregion
    }
}
