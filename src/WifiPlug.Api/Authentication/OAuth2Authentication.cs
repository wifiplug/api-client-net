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
        /// Exchange an authorization code for an ApiAuthentication.
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="clientSecret"></param>
        /// <param name="authorizationCode"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="baseUrl"></param>
        /// <returns></returns>
        public static async Task<OAuth2Authentication> ExchangeAuthorizationCodeAsync(string clientID, string clientSecret, string authorizationCode, CancellationToken cancellationToken = default(CancellationToken), string baseUrl = null)
        {
            // Get tokens
            // Create the form data
            var exchangeContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("client_id", clientID),
                new KeyValuePair<string, string>("client_secret", clientSecret),
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("code", authorizationCode)
            });

            // Create the client
            BaseApiClient client = new ApiClient();

            // Make request
            var response = await client.RequestJsonSerializedWithFormDataAsync<OAuth2TokenResponseEntity>(HttpMethod.Post, $"{baseUrl ?? DefaultUrl}/token", exchangeContent, cancellationToken);

            return new OAuth2Authentication(clientID, clientSecret, response.AccessToken, response.RefreshToken);
        }

        /// <summary>
        /// Refreshes the token.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        protected internal override async Task<bool> ReauthorizeAsync(IBaseApiRequestor client) {
            // Check an access token is provided before attempting.
            if (string.IsNullOrEmpty(_refreshToken))
                return false;

            // Create the form data
            var exchangeContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("client_id", _clientID),
                new KeyValuePair<string, string>("client_secret", _clientSecret),
                new KeyValuePair<string, string>("grant_type", "refresh_token"),
                new KeyValuePair<string, string>("refresh_token", _refreshToken)
            });

            // Create the client
            BaseApiClient authorizationClient = new ApiClient();

            // Make request
            try
            {
                OAuth2TokenResponseEntity response = await authorizationClient.RequestJsonSerializedWithFormDataAsync<OAuth2TokenResponseEntity>(HttpMethod.Post, $"{_baseUrl ?? DefaultUrl}/token", exchangeContent, default(CancellationToken));

                _accessToken = response.AccessToken;
                _refreshToken = response.RefreshToken;

                return true;
            } catch (Exception) {
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
        /// Creates a new OAuth2 authentication object without a refresh token.
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="clientSecret"></param>
        /// <param name="accessToken"></param>
        public OAuth2Authentication(string clientID, string clientSecret, string accessToken)
            : this(clientID, clientSecret, accessToken, "", null)
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
    }

    /// <summary>
    /// Defines an OAuth2Token response entity.
    /// </summary>
    public class OAuth2TokenResponseEntity
    {
        /// <summary>
        /// Gets or sets the accesss token.
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// Gets or sets the refresh token.
        /// </summary>
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        /// <summary>
        /// Gets or sets the number of seconds until the access expires.
        /// </summary>
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        /// <summary>
        /// Gets or sets the date at which the refresh ability will expire.
        /// </summary>
        [JsonProperty("refreshable_until")]
        public DateTime RefreshableUntil { get; set; }

        /// <summary>
        /// Gets or sets the token type.
        /// </summary>
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
    }
}
