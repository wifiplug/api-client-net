// Copyright (C) WIFIPLUG. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Text;

namespace WifiPlug.Api.Schema
{
    /// <summary>
    /// Defines the error codes for the API.
    /// </summary>
    public static class ErrorCodes
    {
        /// <summary>
        /// The login credentials are invalid.
        /// </summary>
        public static readonly string InvalidLogin = "invalid_login";

        /// <summary>
        /// The requested operation cannot complete it's task as an underlying service is unavailable.
        /// You should retry the operation at a later time.
        /// </summary>
        public static readonly string ServiceUnavailable = "service_unavailable";

        /// <summary>
        /// The request attempted to perform an invalid action.
        /// </summary>
        public static readonly string InvalidAction = "invalid_action";

        /// <summary>
        /// No attempted change was submitted, most likely in error.
        /// </summary>
        public static readonly string EmptyRequest = "empty_request";

        /// <summary>
        /// The request field format is invalid.
        /// </summary>
        public static readonly string InvalidField = "invalid_field";

        /// <summary>
        /// The resource already exists or the identifier would result in a duplicate.
        /// </summary>
        public static readonly string AlreadyExists = "already_exists";

        /// <summary>
        /// The resource was not found.
        /// </summary>
        public static readonly string NotFound = "not_found";

        /// <summary>
        /// The target device is not online.
        /// </summary>
        public static readonly string NotOnline = "not_online";

        /// <summary>
        /// The operation requires a user context, but none is available.
        /// </summary>
        public static readonly string NoUser = "no_user";

        /// <summary>
        /// The authentication has expired.
        /// </summary>
        public static readonly string AccessExpired = "access_expired";

        /// <summary>
        /// The request JSON is invalid.
        /// </summary>
        public static readonly string InvalidJson = "invalid_json";

        /// <summary>
        /// No API key is present.
        /// </summary>
        public static readonly string MissingApiKey = "missing_api_key";

        /// <summary>
        /// No API secret is present.
        /// </summary>
        public static readonly string MissingApiSecret = "missing_api_secret";

        /// <summary>
        /// The API key/secret is invalid.
        /// </summary>
        public static readonly string InvalidApiKeys = "invalid_api_keys";

        /// <summary>
        /// The API keypair has been suspended.
        /// </summary>
        public static readonly string SuspendedApiKeys = "suspended_api_keys";

        /// <summary>
        /// The requested method is not available on the target resource.
        /// </summary>
        public static readonly string MethodNotAllowed = "method_not_allowed";

        /// <summary>
        /// An unknown error occured.
        /// </summary>
        public static readonly string InternalServerError = "internal_server_error";
    }
}
