using System;
using System.Net;
using System.Net.Http;

namespace WifiPlug.Api
{
    /// <summary>
    /// Represents a failure response from the API.
    /// </summary>
    public class ApiException : Exception
    {
        private ApiError[] _errors;
        private HttpResponseMessage _response;
        
        /// <summary>
        /// Gets the relevant errors, may be empty.
        /// </summary>
        public ApiError[] Errors {
            get {
                return _errors;
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

        /// <summary>
        /// Gets the response that generated the exception, if any.
        /// </summary>
        public HttpResponseMessage Response {
            get {
                return _response;
            }
        }

        internal ApiException(string message, ApiError[] errors, HttpResponseMessage res) 
            : base(message) {
            _errors = errors;
            _response = res;
        }

        internal ApiException(string message, ApiError[] errors, HttpResponseMessage res, Exception innerException)
            : base(message, innerException) {
            _response = res;
            _errors = errors;
        }
    }
}
