// Copyright (C) WIFIPLUG. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WifiPlug.Api.Entities;

namespace WifiPlug.Api.Authentication
{
    /// <summary>
    /// Provides full OAuth2 authentication.
    /// </summary>
    public class OAuth2Authentication : ApiAuthentication
    {
        #region Constants
        internal const string DefaultUrl = "https://account.wifiplug.co.uk/oauth2";
        #endregion

        #region Fields
        private string _baseUrl;
        private string _clientID;
        private string _clientSecret;
        private string _accessToken;
        private string _refreshToken;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the access token
        /// </summary>
        public string AccessToken {
            get {
                return _accessToken;
            }
        }

        /// <summary>
        /// Gets the refresh token
        /// </summary>
        public string RefreshToken {
            get {
                return _refreshToken;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Applies bearer authentication to an outgoing request.
        /// </summary>
        /// <param name="request">The request.</param>
        public override void Apply(HttpRequestMessage request) {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
        }

        /// <summary>
        /// Deserializes the bearer authentication from a stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public override void Deserialize(Stream stream) {
            using (StreamReader sr = new StreamReader(stream))
            {
                var settings = JsonConvert.DeserializeObject<Dictionary<string, string>>(sr.ReadToEnd());
                
                _accessToken = settings["AccessToken"];
                _refreshToken = settings["RefreshToken"];
            }
        }

        /// <summary>
        /// Serializes the bearer authentication to a stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public override void Serialize(Stream stream) {
            using (StreamWriter sw = new StreamWriter(stream, Encoding.UTF8, 4096, true))
            {
                sw.Write(JsonConvert.SerializeObject(new Dictionary<string, string>()
                {
                    { "AccessToken", _accessToken },
                    { "RefreshToken", _refreshToken }
                }));
            }
        }

        /// <summary>
        /// Exchange an authorization code for an access and refresh token.
        /// </summary>
        /// <param name="clientID">Your client ID</param>
        /// <param name="clientSecret">Your client secret</param>
        /// <param name="authorizationCode">The authorization code.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public static async Task<OAuth2TokenResponseEntity> ExchangeAuthorizationCodeAForTokensAsync(string clientID, string clientSecret, string authorizationCode, CancellationToken cancellationToken = default(CancellationToken), string baseUrl = null)
        {
            // Create the form data
            var exchangeContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("client_id", clientID),
                new KeyValuePair<string, string>("client_secret", clientSecret),
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("code", authorizationCode)
            });

            // Create the client
            OAuth2AuthenticationClient client = new OAuth2AuthenticationClient(baseUrl ?? DefaultUrl);

            // Make request
            return await client.RequestJsonSerializedWithFormDataAsync<OAuth2TokenResponseEntity>(HttpMethod.Post, $"{baseUrl ?? DefaultUrl}/token", exchangeContent, cancellationToken);
        }

        /// <summary>
        /// Exchange an authorization code for an ApiAuthentication.
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="clientSecret"></param>
        /// <param name="authorizationCode"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="baseUrl"></param>
        /// <returns></returns>
        public static async Task<OAuth2Authentication> ExchangeAuthorizationCodeForAuthenticationAsync(string clientID, string clientSecret, string authorizationCode, CancellationToken cancellationToken = default(CancellationToken), string baseUrl = null)
        {
            // Get tokens
            OAuth2TokenResponseEntity response = await ExchangeAuthorizationCodeAForTokensAsync(clientID, clientSecret, authorizationCode, cancellationToken, baseUrl);
            return new OAuth2Authentication(clientID, clientSecret, response.AccessToken, response.RefreshToken);
        }

        /// <summary>
        /// Refreshes the token.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        public override async Task<bool> ReauthorizeAsync(IBaseApiRequestor client) {
            // Create the form data
            var exchangeContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("client_id", _clientID),
                new KeyValuePair<string, string>("client_secret", _clientSecret),
                new KeyValuePair<string, string>("grant_type", "refresh_token"),
                new KeyValuePair<string, string>("refresh_token", _refreshToken)
            });

            // Create the client
            OAuth2AuthenticationClient authorizationClient = new OAuth2AuthenticationClient(_baseUrl ?? DefaultUrl);

            // Make request
            try
            {
                OAuth2TokenResponseEntity response = await authorizationClient.RequestJsonSerializedWithFormDataAsync<OAuth2TokenResponseEntity>(HttpMethod.Post, $"{_baseUrl ?? DefaultUrl}/token", exchangeContent, default(CancellationToken));

                _accessToken = response.AccessToken;
                _refreshToken = response.RefreshToken;

                return true;
            } catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new OAuth2 authentication object without access or refresh tokens.
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="clientSecret"></param>
        public OAuth2Authentication(string clientID, string clientSecret)
            : this(clientID, clientSecret, null, null)
        {
        }

        /// <summary>
        /// Creates a new oAuth2 authentication object with an access and refresh token.
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="clientSecret"></param>
        /// <param name="accessToken"></param>
        /// <param name="refreshToken"></param>
        public OAuth2Authentication(string clientID, string clientSecret, string accessToken, string refreshToken)
            : this(clientID, clientSecret, accessToken, refreshToken, null)
        {
        }

        /// <summary>
        /// Creates a new OAuth2 Authentication object.
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="clientSecret"></param>
        /// <param name="accessToken"></param>
        /// <param name="refreshToken"></param>
        /// <param name="baseUrl"></param>
        public OAuth2Authentication(string clientID, string clientSecret, string accessToken, string refreshToken, string baseUrl)
        {
            _clientID = clientID;
            _clientSecret = clientSecret;
            _accessToken = accessToken;
            _refreshToken = refreshToken;

            if (baseUrl != null)
                _baseUrl = baseUrl;
            else
                _baseUrl = DefaultUrl;
        }
        #endregion



        /// <summary>
        /// Defines a HTTP client to be used for OAuth2 requests
        /// </summary>
        private class OAuth2AuthenticationClient : HttpClient
        {
            private string _baseUrl;
            private HttpClient _httpClient;
            private int _retryCount = 3;
            private TimeSpan _retryDelay = TimeSpan.FromSeconds(3);

            /// <summary>
            /// Sends a REST request to the API, includes retry logic and reauthorisation.
            /// </summary>
            /// <param name="method">The target method.</param>
            /// <param name="path">The target path.</param>
            /// <param name="content">The request content, or null.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>The response message.</returns>
            async Task<HttpResponseMessage> RequestAsync(HttpMethod method, string path, HttpContent content, CancellationToken cancellationToken)
            {
                for (int i = 0; i < _retryCount; i++)
                {
                    try
                    {
                        return await (RawRequestAsync(method, path, content, cancellationToken).ConfigureAwait(false));
                    }
                    catch (OAuth2AuthenticationException ex)
                    {
                        if (ex.StatusCode == HttpStatusCode.BadGateway && i < _retryCount - 1)
                        {
                            await Task.Delay(_retryDelay, cancellationToken).ConfigureAwait(false);
                            continue;
                        }
                        else
                        {
                            throw;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }

                throw new OAuth2AuthenticationException("Unreachable", new OAuth2AuthenticationError[] { new OAuth2AuthenticationError("Unreachable", "Could not reach account service") });
            }

            /// <summary>
            /// Requesta JSON object response.
            /// </summary>
            /// <param name="method"></param>
            /// <param name="path"></param>
            /// <param name="content"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public async Task<JObject> RequestJsonObjectAsync(HttpMethod method, string path, HttpContent content, CancellationToken cancellationToken)
            {
                HttpResponseMessage response = await RequestAsync(method, path, content, cancellationToken).ConfigureAwait(false);

                if (!response.Content.Headers.ContentType.MediaType.StartsWith("application/json", StringComparison.CurrentCultureIgnoreCase))
                    throw new ApiException("Invalid server response", new ApiError[0], response);

                return JObject.Parse(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
            }

            /// <summary>
            /// Raw request to ther OAuth2 Authentication service.
            /// </summary>
            /// <param name="method"></param>
            /// <param name="path"></param>
            /// <param name="content"></param>
            /// <param name="cancelationToken"></param>
            /// <returns></returns>
            public async Task<HttpResponseMessage> RawRequestAsync(HttpMethod method, string path, HttpContent content, CancellationToken cancelationToken)
            {
                // throw if cancelled
                cancelationToken.ThrowIfCancellationRequested();

                // Create a request message
                HttpRequestMessage req = new HttpRequestMessage(method, path);

                // Add the body
                if (method != HttpMethod.Get && content != null)
                    req.Content = content;

                // Send the request
                HttpResponseMessage response = await _httpClient.SendAsync(req, cancelationToken).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    return response;
                }
                else
                {
                    cancelationToken.ThrowIfCancellationRequested();

                    // Parse the error response
                    string errMessage;
                    OAuth2AuthenticationError[] errArr;

                    try
                    {
                        JObject obj = JObject.Parse(await response.Content.ReadAsStringAsync().ConfigureAwait(false));

                        // Convert the object into error objects
                        JArray errJson = (JArray)obj["errors"];
                        List<OAuth2AuthenticationError> errList = new List<OAuth2AuthenticationError>(errJson.Count);

                        foreach (JObject err in errJson)
                        {
                            // get additional data
                            Dictionary<string, object> data = new Dictionary<string, object>();

                            foreach (KeyValuePair<string, JToken> kv in err)
                            {
                                if (kv.Key.Equals("error", StringComparison.CurrentCultureIgnoreCase) || kv.Key.Equals("message", StringComparison.CurrentCultureIgnoreCase) || kv.Key.Equals("field", StringComparison.CurrentCultureIgnoreCase))
                                    continue;

                                // convert type if required
                                object o = kv.Value;

                                switch (kv.Value.Type)
                                {
                                    case JTokenType.Boolean:
                                        o = (bool)kv.Value;
                                        break;
                                    case JTokenType.Bytes:
                                        o = (byte[])kv.Value;
                                        break;
                                    case JTokenType.Date:
                                        o = (DateTime)kv.Value;
                                        break;
                                    case JTokenType.Float:
                                        o = (float)kv.Value;
                                        break;
                                    case JTokenType.Guid:
                                        o = (Guid)kv.Value;
                                        break;
                                    case JTokenType.Integer:
                                        o = Convert.ToInt32(kv.Value);
                                        break;
                                    case JTokenType.Null:
                                        o = null;
                                        break;
                                    case JTokenType.String:
                                        o = (string)kv.Value;
                                        break;
                                    case JTokenType.TimeSpan:
                                        o = (TimeSpan)kv.Value;
                                        break;
                                    case JTokenType.Uri:
                                        o = (Uri)kv.Value;
                                        break;
                                }

                                // set data
                                data[kv.Key] = kv.Value;
                            }

                            // add error to list
                            errList.Add(new OAuth2AuthenticationError((string)err["error"], (string)err["message"], (string)err["field"], data));
                        }

                        // assign
                        errArr = errList.ToArray();
                        errMessage = (errArr.Length > 1 ? $"{errList.Count} errors occured" : errArr[0].Message) ?? "Unspecified error";
                    }
                    catch (Exception)
                    {
                        throw new OAuth2AuthenticationException(string.Format("Invalid server response - {0} {1}", (int)response.StatusCode, response.StatusCode), new OAuth2AuthenticationError[0], response);
                    }

                    // throw api exception
                    throw new OAuth2AuthenticationException(errMessage, errArr, response);
                }
            }

            /// <summary>
            /// Requests a JSON object with no response.
            /// </summary>
            /// <param name="method">The target method.</param>
            /// <param name="path">The target path.</param>
            /// <param name="body">The JSON body.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns></returns>
            Task RequestJsonAsync(HttpMethod method, string path, JObject body, CancellationToken cancellationToken)
            {
                return RequestAsync(method, path, new StringContent(body.ToString(), Encoding.UTF8, "application/json"), cancellationToken);
            }

            /// <summary>
            /// Requests a JSON object with a JSON object response.
            /// </summary>
            /// <param name="method">The target method.</param>
            /// <param name="path">The target path.</param>
            /// <param name="body">The JSON body.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>The JSON object response.</returns>
            Task<JObject> RequestJsonObjectAsync(HttpMethod method, string path, JObject body, CancellationToken cancellationToken)
            {
                return RequestJsonObjectAsync(method, path, new StringContent(body.ToString(), Encoding.UTF8, "application/json"), cancellationToken);
            }

            /// <summary>
            /// Requests a serialized object with a serialized object
            /// </summary>
            /// <typeparam name="TReq">The request object type.</typeparam>
            /// <param name="method">The target method.</param>
            /// <param name="path">The target path.</param>
            /// <param name="body">The object to be serialized.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns></returns>
            public async Task<TReq> RequestJsonSerializedAsync<TReq>(HttpMethod method, string path, TReq body, CancellationToken cancellationToken)
            {
                HttpResponseMessage response = await RequestAsync(method, path, new StringContent(JsonConvert.SerializeObject(body)), cancellationToken).ConfigureAwait(false);

                if (!response.Content.Headers.ContentType.MediaType.StartsWith("application/json", StringComparison.CurrentCultureIgnoreCase))
                    throw new ApiException("Invalid server response", new ApiError[0], response);

                return JsonConvert.DeserializeObject<TReq>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
            }

            /// <summary>
            /// Request a serialized object with a form data request.
            /// </summary>
            /// <typeparam name="TReq"></typeparam>
            /// <param name="method"></param>
            /// <param name="path"></param>
            /// <param name="content"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public async Task<TReq> RequestJsonSerializedWithFormDataAsync<TReq>(HttpMethod method, string path, HttpContent content, CancellationToken cancellationToken)
            {
                HttpResponseMessage response = await RequestAsync(method, path, content, cancellationToken).ConfigureAwait(false);

                if (!response.Content.Headers.ContentType.MediaType.StartsWith("application/json", StringComparison.CurrentCultureIgnoreCase))
                    throw new ApiException("Invalid server response", new ApiError[0], response);

                return JsonConvert.DeserializeObject<TReq>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
            }

            /// <summary>
            /// Requests a serialized object.
            /// </summary>
            /// <typeparam name="TReq"></typeparam>1
            /// <param name="method"></param>
            /// <param name="path"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public async Task<TReq> RequestJsonSerializedAsync<TReq>(HttpMethod method, string path, CancellationToken cancellationToken)
            {
                HttpResponseMessage response = await (RequestAsync(method, path, new StringContent("", Encoding.UTF8), cancellationToken).ConfigureAwait(false));

                if (!response.Content.Headers.ContentType.MediaType.StartsWith("application/json", StringComparison.CurrentCultureIgnoreCase))
                    throw new ApiException("Invalid server response", new ApiError[0], response);

                return JsonConvert.DeserializeObject<TReq>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
            }

            public OAuth2AuthenticationClient(string baseUrl)
            {
                _httpClient = new HttpClient();
                _baseUrl = baseUrl;
            }
        }
    }

    /// <summary>
    /// Represents a failure response from the OAuth2 service
    /// </summary>
    public class OAuth2AuthenticationException : Exception
    {
        private OAuth2AuthenticationError[] _errors;
        private HttpResponseMessage _response;

        /// <summary>
        /// Gets the relevanty errors, may be empty
        /// </summary>
        public OAuth2AuthenticationError[] Errors {
            get {
                return _errors;
            }
        }

        /// <summary>
        /// Gets the response that generated the exception, if any.
        /// </summary>
        public HttpResponseMessage Response {
            get {
                return _response;
            }
        }

        /// <summary>
        /// Gets the status code of the response that generated the exception, if any.
        /// </summary>
        public HttpStatusCode StatusCode {
            get {
                return _response == null ? 0 : _response.StatusCode;
            }
        }

        internal OAuth2AuthenticationException(string message, OAuth2AuthenticationError[] errors)
            : base(message)
        {
            _errors = errors;
        }

        internal OAuth2AuthenticationException(string message, OAuth2AuthenticationError[] errors, HttpResponseMessage res)
            : base(message)
        {
            _errors = errors;
            _response = res;
        }

        internal OAuth2AuthenticationException(string message, OAuth2AuthenticationError[] errors, HttpResponseMessage res, Exception innerException)
            : base(message, innerException)
        {
            _errors = errors;
            _response = res;
        }
    }

    /// <summary>
    /// Defines an error object returned if an OAuth2 request fails.
    /// </summary>
    public sealed class OAuth2AuthenticationError
    {
        #region Properties
        /// <summary>
        /// Gets the code.
        /// </summary>
        public string Error { get; private set; }

        /// <summary>
        /// Gets the message
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Gets the field, if any.
        /// </summary>
        public string Field { get; private set; }

        /// <summary>
        /// Gets any addition data in the error.
        /// </summary>
        public IDictionary<string, object> Data { get; private set; }
        #endregion

        #region Constructors
        internal OAuth2AuthenticationError(string error, string message)
            : this(error, message, null, null)
        {
        }

        internal OAuth2AuthenticationError(string error, string message, IDictionary<string, object> data)
            : this(error, message, null, data)
        {
        }

        internal OAuth2AuthenticationError(string error, string message, string field, IDictionary<string, object> data)
        {
            Error = error;
            Message = message;
            Field = field;
            Data = Data ?? new Dictionary<string, object>();
        }
        #endregion
    }
}
