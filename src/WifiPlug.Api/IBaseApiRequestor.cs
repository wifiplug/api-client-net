// Copyright (C) WIFIPLUG. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WifiPlug.Api
{
    /// <summary>
    /// Defines an interface for the base API client functionality.
    /// </summary>
    public interface IBaseApiRequestor : IBaseApiClient
    {
        /// <summary>
        /// Sends a REST request to the API, includes retry logic and reauthorisation.
        /// </summary>
        /// <param name="method">The target method.</param>
        /// <param name="path">The target path.</param>
        /// <param name="content">The request content, or null.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The response message.</returns>
        Task<HttpResponseMessage> RequestAsync(HttpMethod method, string path, HttpContent content, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Sends the raw REST request to the API, includes error handling logic but no reauthorisation or retry logic.
        /// </summary>
        /// <param name="method"></param>
        /// <param name="path"></param>
        /// <param name="content"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> RawRequestAsync(HttpMethod method, string path, HttpContent content, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Requests a string response.
        /// </summary>
        /// <param name="method">The target method.</param>
        /// <param name="path">The target path.</param>
        /// <param name="content">The request content, or null.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The response string.</returns>
        Task<string> RequestStringAsync(HttpMethod method, string path, HttpContent content = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Requests a JSON object response.
        /// </summary>
        /// <param name="method">The target method.</param>
        /// <param name="path">The target path.</param>
        /// <param name="content">The request content, or null.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The response message.</returns>
        /// <returns>The JSON object response.</returns>
        Task<JObject> RequestJsonObjectAsync(HttpMethod method, string path, HttpContent content = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Requests a JSON object with no response.
        /// </summary>
        /// <param name="method">The target method.</param>
        /// <param name="path">The target path.</param>
        /// <param name="body">The JSON body.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task RequestJsonAsync(HttpMethod method, string path, JObject body, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Requests a JSON object with a JSON object response.
        /// </summary>
        /// <param name="method">The target method.</param>
        /// <param name="path">The target path.</param>
        /// <param name="body">The JSON body.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The JSON object response.</returns>
        Task<JObject> RequestJsonObjectAsync(HttpMethod method, string path, JObject body, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Requests a serialized object with no response.
        /// </summary>
        /// <typeparam name="TReq">The request object type.</typeparam>
        /// <param name="method">The target method.</param>
        /// <param name="path">The target path.</param>
        /// <param name="body">The object to be serialized.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task RequestJsonSerializedAsync<TReq>(HttpMethod method, string path, TReq body, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Requests a serialized object with a JSON object response.
        /// </summary>
        /// <typeparam name="TReq">The request object type.</typeparam>
        /// <param name="method">The target method.</param>
        /// <param name="path">The target path.</param>
        /// <param name="body">The object to be serialized.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The JSON object response.</returns>
        Task<JObject> RequestJsonSerializedObjectAsync<TReq>(HttpMethod method, string path, TReq body, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Requests a JSON object with a serialized object response.
        /// </summary>
        /// <typeparam name="TRes">The response object type.</typeparam>
        /// <param name="method">The target method.</param>
        /// <param name="path">The target path.</param>
        /// <param name="body">The JSON body.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The response object.</returns>
        Task<TRes> RequestJsonSerializedAsync<TRes>(HttpMethod method, string path, JObject body, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Requests a serialized object with a serialized object response.
        /// </summary>
        /// <typeparam name="TReq">The request object type.</typeparam>
        /// <typeparam name="TRes">The response object type.</typeparam>
        /// <param name="method">The target method.</param>
        /// <param name="path">The target path.</param>
        /// <param name="body">The JSON body.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The response object.</returns>
        Task<TRes> RequestJsonSerializedAsync<TReq, TRes>(HttpMethod method, string path, TReq body, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Requests a serialized object response.
        /// </summary>
        /// <typeparam name="TRes">The response object type.</typeparam>
        /// <param name="method">The target method.</param>
        /// <param name="path">The target path.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The response object.</returns>
        Task<TRes> RequestJsonSerializedAsync<TRes>(HttpMethod method, string path, CancellationToken cancellationToken = default(CancellationToken));
    }
}
