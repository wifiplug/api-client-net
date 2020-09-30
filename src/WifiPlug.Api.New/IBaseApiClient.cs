using System;
using System.Net.Http;

namespace WifiPlug.Api.New
{
    public interface IBaseApiClient
    {
        #region Properties
        /// <summary>
        /// Gets or sets the base URI to use for the API.
        /// </summary>
        Uri BaseApiUri { get; set; }

        /// <summary>
        /// Gets the API client's underlying HTTP client.
        /// </summary>
        HttpClient HttpClient { get; }

        /// <summary>
        /// Gets or sets how many times an operation should be retried for transient failures.
        /// </summary>
        int RetryCount { get; set; }

        /// <summary>
        /// Gets or sets how long to wait between retrying operations.
        /// </summary>
        TimeSpan RetryDelay { get; set; }
        #endregion
    }
}
