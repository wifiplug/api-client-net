// Copyright (C) WIFIPLUG. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Text;

namespace WifiPlug.Api.Scopes
{
    /// <summary>
    /// Represent a session scope, does not work with most API keys.
    /// </summary>
    public class SessionScope : IEventScope
    {
        private string _token;

        /// <summary>
        /// Gets the session token.
        /// </summary>
        public string Token {
            get {
                return _token;
            }
        }

        /// <summary>
        /// Gets the string representation of this scope.
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return $"session-{_token}";
        }

        /// <summary>
        /// Creates a new session token scope for the event client.
        /// </summary>
        /// <param name="token">The token.</param>
        public SessionScope(string token) {
            _token = token;
        }
    }
}
