// Copyright (C) WIFIPLUG. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WifiPlug.Api.Authentication
{
    /// <summary>
    /// Provides full OAuth2 authentication.
    /// </summary>
    public sealed class OAuth2Authentication : BearerAuthentication
    {
        #region Constants
        internal const string TokenUrl = "https://account.wifiplug.co.uk/oauth2/token";
        #endregion

        #region Fields
        private Uri _tokenUri;
        private string _refreshToken;
        #endregion

        #region Methods
        /// <summary>
        /// Applies bearer authentication to an outgoing request.
        /// </summary>
        /// <param name="request">The request.</param>
        public override void Apply(HttpRequestMessage request) {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _bearerToken);
        }

        /// <summary>
        /// Deserializes the bearer authentication from a stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public override void Deserialize(Stream stream) {
            // deserialize bearer token
            base.Deserialize(stream);

            // read refresh token
            BinaryReader reader = new BinaryReader(stream, Encoding.UTF8, true);

           
        }

        /// <summary>
        /// Serializes the bearer authentication to a stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public override void Serialize(Stream stream) {
            // serialize bearer token
            base.Serialize(stream);

            // write refresh token
            BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8, true);
        }

        /// <summary>
        /// Refreshes the token.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        public override Task<bool> ReauthorizeAsync(IBaseApiClient client) {
            // create refresh client
            HttpClient refreshClient = new HttpClient();
            refreshClient.BaseAddress = _tokenUri;

            // make request
            //refreshClient.PostAsync("/")

            return Task.FromResult(false);
        }
        #endregion

        #region Entities
        class RefreshAccessTokenEntity
        {
            [JsonProperty("grant_type")]
            public string GrantType { get; } = "refresh_token";

            [JsonProperty("refresh_token")]
            public string RefreshToken { get; set; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates an empty oAuth2 authentication object.
        /// </summary>
        public OAuth2Authentication()
            : base() {
        }

        /// <summary>
        /// Creates a new oAuth2 authentication object from an access and refresh token.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="refreshToken">The refresh token.</param>
        public OAuth2Authentication(string accessToken, string refreshToken)
            : this(null, accessToken, refreshToken) {
            _refreshToken = refreshToken;
        }

        /// <summary>
        /// Creates a new oAuth2 authentication object from an access and refresh token.
        /// </summary>
        /// <param name="tokenUri">The optional token URI.</param>
        /// <param name="accessToken">The access token.</param>
        /// <param name="refreshToken">The refresh token.</param>
        public OAuth2Authentication(string tokenUri, string accessToken, string refreshToken) {
            _tokenUri = tokenUri == null ? new Uri(TokenUrl) : new Uri(tokenUri);
            _refreshToken = refreshToken;
        }
        #endregion
    }
}
