// Copyright (C) WIFIPLUG. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WifiPlug.Api
{
    /// <summary>
    /// Provides functionality to authenticate with the API.
    /// </summary>
    public abstract class ApiAuthentication
    {
        /// <summary>
        /// Applies authentication to an outgoing request.
        /// </summary>
        /// <param name="request">The request.</param>
        public abstract void Apply(HttpRequestMessage request);

        /// <summary>
        /// Deserializes from the stream into the authentication object.
        /// </summary>
        /// <param name="stream">The input stream.</param>
        public abstract void Deserialize(Stream stream);

        /// <summary>
        /// Serializes the authentication object for peristent storage.
        /// </summary>
        /// <param name="stream">The output stream.</param>
        public abstract void Serialize(Stream stream);

        /// <summary>
        /// Attempt to refresh the authorization. 
        /// </summary>
        /// <param name="client">The client.</param>
        /// <returns>If the request should be attempted again.</returns>
        public abstract Task<bool> ReauthorizeAsync(IBaseApiRequestor client);

        /// <summary>
        /// Serializes the authentication object for persistent storage.
        /// </summary>
        /// <returns>The authentication data.</returns>
        public string Serialize() {
            using (MemoryStream ms = new MemoryStream()) {
                // serialize
                Serialize(ms);

                // convert
                ms.Flush();
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        /// <summary>
        /// Deserializes from the data into the authentication object.
        /// </summary>
        /// <param name="data">The data.</param>
        public void Deserialize(string data) {
            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(data))) {
                Deserialize(ms);
            }
        }
    }
}
