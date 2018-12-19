// Copyright (C) WIFIPLUG. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WifiPlug.Api.Authentication
{
    /// <summary>
    /// Provides session token authentication.
    /// </summary>
    public class SessionAuthentication : ApiAuthentication
    {
        #region Fields
        private string _token;
        #endregion

        #region Methods
        /// <summary>
        /// Applies session authentication to an outgoing request.
        /// </summary>
        /// <param name="request">The request.</param>
        public override void Apply(HttpRequestMessage request) {
            request.Headers.Add("X-Session-Token", _token);
        }

        /// <summary>
        /// Deserializes the session authentication information.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public override void Deserialize(Stream stream) {
            BinaryReader reader = new BinaryReader(stream, Encoding.UTF8, true);

            // read length
            short length = reader.ReadInt16();

            if (length < 0 || length > 512)
                throw new FormatException("The bearer token is too long");

            // read token
            byte[] tokenBytes = reader.ReadBytes(length);

            _token = Encoding.UTF8.GetString(tokenBytes);
        }

        /// <summary>
        /// Serializes the session authentication information.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public override void Serialize(Stream stream) {
            BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8, true);

            // generate data
            byte[] tokenBytes = Encoding.UTF8.GetBytes(_token);

            // write
            writer.Write((short)tokenBytes.Length);
            writer.Write(tokenBytes);
        }

        /// <summary>
        /// Reauthorizes the session authentication.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <remarks>Not supported currently.</remarks>
        /// <returns></returns>
        public override Task<bool> ReauthorizeAsync(IBaseApiClient client) {
            return Task.FromResult(false);
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates an empty session authentication object.
        /// </summary>
        public SessionAuthentication() {
            _token = string.Empty;
        }

        /// <summary>
        /// Creates a new session authentication object from a token.
        /// </summary>
        /// <param name="token">The token.</param>
        public SessionAuthentication(string token) {
            _token = token;
        }
        #endregion
    }
}
