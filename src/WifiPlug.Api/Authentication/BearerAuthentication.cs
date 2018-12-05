// Copyright (C) WIFIPLUG. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

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
    /// Provides bearer authentication, to remain library agnostic it does not support refresh tokens.
    /// Inherit <see cref="BearerAuthentication"/> and implement <see cref="BearerAuthentication.ReauthorizeAsync(ApiClient)"/> to add this functionality.
    /// </summary>
    public class BearerAuthentication : ApiAuthentication
    {
        #region Fields
        /// <summary>
        /// The oAuth 2 bearer token.
        /// </summary>
        protected string _bearerToken;
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
            BinaryReader reader = new BinaryReader(stream, Encoding.UTF8, true);

            // read length
            short bearerLength = reader.ReadInt16();

            if (bearerLength < 0 || bearerLength > 512)
                throw new FormatException("The access token is too long");

            // read token
            byte[] bearerTokenBytes = reader.ReadBytes(bearerLength);
            _bearerToken = Encoding.UTF8.GetString(bearerTokenBytes);
        }
        
        /// <summary>
        /// Serializes the bearer authentication to a stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public override void Serialize(Stream stream) {
            BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8, true);

            // generate data
            byte[] bearerTokenBytes = Encoding.UTF8.GetBytes(_bearerToken);

            // write
            writer.Write((short)bearerTokenBytes.Length);
            writer.Write(bearerTokenBytes);
        }

        /// <summary>
        /// No support for refresh tokens.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        public override Task<bool> ReauthorizeAsync(ApiClient client) {
            return Task.FromResult(false);
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates an empty bearer authentication object.
        /// </summary>
        public BearerAuthentication() {
            _bearerToken = string.Empty;
        }

        /// <summary>
        /// Creates a new bearer authentication object from a bearer token.
        /// </summary>
        /// <param name="bearerToken">The bearer token.</param>
        public BearerAuthentication(string bearerToken) {
            _bearerToken = bearerToken;
        }
        #endregion
    }
}
