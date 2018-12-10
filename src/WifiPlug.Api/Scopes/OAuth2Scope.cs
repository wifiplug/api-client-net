// Copyright (C) WIFIPLUG. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Text;

namespace WifiPlug.Api.Scopes
{
    /// <summary>
    /// Represent an oAuth 2 scope.
    /// </summary>
    public class OAuth2Scope : IEventScope
    {
        private string _accessToken;

        /// <summary>
        /// Gets the session token.
        /// </summary>
        public string AccessToken {
            get {
                return _accessToken;
            }
        }

        /// <summary>
        /// Gets the string representation of this scope.
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return $"oauth2-{_accessToken}";
        }

        /// <summary>
        /// Creates a new oAuth 2 scope for the event client.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        public OAuth2Scope(string accessToken) {
            _accessToken = accessToken;
        }
    }
}
