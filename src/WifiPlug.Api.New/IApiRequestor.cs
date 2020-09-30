using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WifiPlug.Api.New.Entities;

namespace WifiPlug.Api.New
{
    public interface IApiRequestor
    {
        #region Public Methods
        /// <summary>
        /// Sends a raw REST request to the API. Includes error handling logic but no reauthorisation or retry logic.
        /// </summary>
        /// <param name="method">The request method.</param>
        /// <param name="path">The request path.</param>
        /// <param name="content">The request content, if any.</param>
        Task<HttpResponseMessage> RawRequestAsync(HttpMethod method, string path, HttpContent content, CancellationToken cancellationToken);

        /// <summary>
        /// Sends a REST request to the API. Includes retry logic and reauthorization.
        /// </summary>
        /// <param name="method">The request method.</param>
        /// <param name="path">The request path.</param>
        /// <param name="content">The request content, if any.</param>
        Task<HttpResponseMessage> RequestAsync<TRequest>(HttpMethod method, string path, TRequest content, CancellationToken cancellationToken);

        /// <summary>
        /// Sends a REST request to the API. Includes retry logic and reauthorization.
        /// </summary>
        /// <param name="method">The request method.</param>
        /// <param name="path">The request path.</param>
        /// <param name="content">The request content, if any.</param>
        Task<HttpResponseMessage> RequestAsync(HttpMethod method, string path, HttpContent content, CancellationToken cancellationToken);

        /// <summary>
        /// Get all available items from all pages.
        /// </summary>
        /// <param name="path">The request path without query parameters.</param>
        Task<TEntity[]> RequestEntireListJsonSerializedAsync<TEntity>(string path, CancellationToken cancellationToken);

        /// <summary>
        /// Request a serialized object response.
        /// </summary>
        /// <param name="method">The request method.</param>
        /// <param name="path">The request path.</param>
        Task<TResponse> RequestJsonSerializedAsync<TResponse>(HttpMethod method, string path, CancellationToken cancellationToken);

        /// <summary>
        /// Request a serialized object response with serialized content.
        /// </summary>
        /// <param name="method">The request method.</param>
        /// <param name="path">The request path.</param>
        /// <param name="content">The request content.</param>
        Task<TResponse> RequestJsonSerializedAsync<TRequest, TResponse>(HttpMethod method, string path, TRequest content, CancellationToken cancellationToken);

        /// <summary>
        /// Request a serialized object response with content.
        /// </summary>
        /// <param name="method">The request method.</param>
        /// <param name="path">The request path.</param>
        /// <param name="content">The request content.</param>
        Task<TResponse> RequestJsonSerializedAsync<TResponse>(HttpMethod method, string path, HttpContent content, CancellationToken cancellationToken);

        /// <summary>
        /// Get a page of items.
        /// </summary>
        /// /// <param name="cursor">The cursor for the next batch of results. Minimum: 1.</param>
        /// <param name="limit">The maximum number of items that can be returned. Minimum: 1, maximum: 50.</param>
        /// <param name="path">The request path without request parameters.</param>
        Task<ResultResponseEntity<TEntity>> RequestResultResponseJsonSerializedAsync<TEntity>(string cursor, int limit, string path, CancellationToken cancellationToken);
        #endregion
    }
}
