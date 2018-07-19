using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WifiPlug.Api.Authentication;
using WifiPlug.Api.Entities;
using WifiPlug.Api.Operations;

namespace WifiPlug.Api
{
    /// <summary>
    /// Provides access to the WIFIPLUG API services and systems.
    /// </summary>
    public class ApiClient
    {
        #region Constants
        internal const string API_URL = "https://api.wifiplug.co.uk/v1.0/";
        #endregion

        #region Fields
        private string _apiKey = null;
        private string _apiSecret = null;
        private ApiAuthentication _authentication;
        private HttpClient _client = null;
        private int _retryCount = 3;
        private TimeSpan _retryDelay = TimeSpan.FromSeconds(3);
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the retry count for transient failures.
        /// </summary>
        public int RetryCount {
            get {
                return _retryCount;
            } set {
                _retryCount = value;
            }
        }

        /// <summary>
        /// Gets or sets the retry delay.
        /// </summary>
        public TimeSpan RetryDelay {
            get {
                return _retryDelay;
            } set {
                _retryDelay = value;
            }
        }

        /// <summary>
        /// Gets or sets the authentication method.
        /// </summary>
        public ApiAuthentication Authentication {
            get {
                return _authentication;
            } set {
                _authentication = value;
            }
        }

        /// <summary>
        /// Gets or sets the timeout for requests.
        /// </summary>
        public TimeSpan Timeout {
            get {
                return _client.Timeout;
            } set {
                _client.Timeout = value;
            }
        }

        /// <summary>
        /// Gets the underlying http client.
        /// </summary>
        public HttpClient Client {
            get {
                return _client;
            }
        }

        /// <summary>
        /// Gets or sets the base address.
        /// </summary>
        public Uri BaseAddress {
            get {
                return _client.BaseAddress;
            } set {
                _client.BaseAddress = value;
            }
        }

        /// <summary>
        /// Gets the device operations.
        /// </summary>
        public virtual IDeviceOperations Devices { get; protected set; }

        /// <summary>
        /// Gets the session operations.
        /// </summary>
        public virtual ISessionOperations Sessions { get; protected set; }

        /// <summary>
        /// Gets the user operations.
        /// </summary>
        public virtual IUserOperations Users { get; protected set; }

        /// <summary>
        /// Gets the group operations.
        /// </summary>
        public virtual IGroupOperations Groups { get; protected set; }

        /// <summary>
        /// Gets the event operations.
        /// </summary>
        public virtual IEventOperations Events { get; protected set; }
        #endregion

        #region Rest Methods
        /// <summary>
        /// Sends a REST request.
        /// </summary>
        /// <param name="method">The target method.</param>
        /// <param name="path">The target path.</param>
        /// <param name="content">The request content, or null.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The response message.</returns>
        protected internal async Task<HttpResponseMessage> RequestAsync(HttpMethod method, string path, HttpContent content, CancellationToken cancellationToken = default(CancellationToken)) {
            bool hasReauthorised = false;

            for (int i = 0; i < _retryCount; i++) {
                try {
                    return await RawRequestAsync(method, path, content, cancellationToken);
                } catch(ApiException ex) {
                    if (ex.StatusCode == HttpStatusCode.BadGateway && i < _retryCount - 1) {
                        await Task.Delay(_retryDelay, cancellationToken);
                        continue;
                    } else if (ex.StatusCode == HttpStatusCode.Unauthorized && _authentication != null && !hasReauthorised) {
                        // try and reauthorize
                        bool success = await _authentication.ReauthorizeAsync(this);

                        // if we're successful we don't count this as a retry, note that we store this
                        // this prevents an infinite loop and gives us retryCount + 1 if we had to reauthorise
                        if (success) {
                            i--;
                            hasReauthorised = true;
                        } else {
                            throw;
                        }
                    } else {
                        throw;
                    }
                }
            }

            throw new NotImplementedException("Unreachable");
        }

        private async Task<HttpResponseMessage> RawRequestAsync(HttpMethod method, string path, HttpContent content, CancellationToken cancellationToken = default(CancellationToken)) {
            // throw if cancelled
            cancellationToken.ThrowIfCancellationRequested();

            // create a request message
            HttpRequestMessage req = new HttpRequestMessage(method, path);

            // assign authentication
            if (_authentication != null)
                _authentication.Apply(req);

            // assign body
            if (content == null)
                req.Content = new StringContent("", Encoding.UTF8);
            else if (method != HttpMethod.Get)
                req.Content = content;

            // send
            HttpResponseMessage response = await _client.SendAsync(req, cancellationToken).ConfigureAwait(false);

            if (response.IsSuccessStatusCode) {
                return response;
            } else {
                // throw if cancelled
                cancellationToken.ThrowIfCancellationRequested();

                // parse failure object
                string errMessage;
                ApiError[] errArr;

                try {
                    // parse the response error object
                    JObject obj = JObject.Parse(await response.Content.ReadAsStringAsync().ConfigureAwait(false));

                    // convert to list of ApiError objects
                    JArray errJson = (JArray)obj["errors"];
                    List<ApiError> errList = new List<ApiError>(errJson.Count);
                    
                    foreach(JObject err in errJson) {
                        // get additional data
                        Dictionary<string, object> data = new Dictionary<string, object>();

                        foreach(KeyValuePair<string, JToken> kv in err) {
                            if (kv.Key.Equals("error", StringComparison.CurrentCultureIgnoreCase) || kv.Key.Equals("message", StringComparison.CurrentCultureIgnoreCase))
                                continue;

                            // convert type if required
                            object o = kv.Value;

                            switch(kv.Value.Type) {
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
                        errList.Add(new ApiError((string)err["error"], (string)err["message"], data));
                    }

                    // assign
                    errArr = errList.ToArray();
                    errMessage = (errArr.Length > 1 ? $"{errList.Count} errors occured" : errArr[0].Message) ?? "Unspecified error";
                } catch(Exception) {
                    throw new ApiException(string.Format("Invalid server response - {0} {1}", (int)response.StatusCode, response.StatusCode), new ApiError[0], response);
                }

                // throw api exception
                throw new ApiException(errMessage, errArr, response);
            }
        }

        /// <summary>
        /// Requests a string response.
        /// </summary>
        /// <param name="method">The target method.</param>
        /// <param name="path">The target path.</param>
        /// <param name="content">The request content, or null.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The response string.</returns>
        protected internal async Task<string> RequestStringAsync(HttpMethod method, string path, HttpContent content = null, CancellationToken cancellationToken = default(CancellationToken)) {
            return await (
                await RequestAsync(method, path, content, cancellationToken).ConfigureAwait(false))
                .Content.ReadAsStringAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Requests a JSON object response.
        /// </summary>
        /// <param name="method">The target method.</param>
        /// <param name="path">The target path.</param>
        /// <param name="content">The request content, or null.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The response message.</returns>
        /// <returns>The JSON object response.</returns>
        protected internal async Task<JObject> RequestJsonObjectAsync(HttpMethod method, string path, HttpContent content = null, CancellationToken cancellationToken = default(CancellationToken))  {
            HttpResponseMessage response = await RequestAsync(method, path, content, cancellationToken).ConfigureAwait(false);

            if (!response.Content.Headers.ContentType.MediaType.StartsWith("application/json", StringComparison.CurrentCultureIgnoreCase))
                throw new ApiException("Invalid server response", new ApiError[0], response);

            return JObject.Parse(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
        }

        /// <summary>
        /// Requests a JSON object with no response.
        /// </summary>
        /// <param name="method">The target method.</param>
        /// <param name="path">The target path.</param>
        /// <param name="body">The JSON body.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        protected internal Task RequestJsonAsync(HttpMethod method, string path, JObject body, CancellationToken cancellationToken = default(CancellationToken)) {
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
        protected internal Task<JObject> RequestJsonObjectAsync(HttpMethod method, string path, JObject body, CancellationToken cancellationToken = default(CancellationToken)) {
            return RequestJsonObjectAsync(method, path, new StringContent(body.ToString(), Encoding.UTF8, "application/json"));
        }

        /// <summary>
        /// Requests a serialized object with no response.
        /// </summary>
        /// <typeparam name="TReq">The request object type.</typeparam>
        /// <param name="method">The target method.</param>
        /// <param name="path">The target path.</param>
        /// <param name="body">The object to be serialized.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        protected internal Task RequestJsonSerializedAsync<TReq>(HttpMethod method, string path, TReq body, CancellationToken cancellationToken = default(CancellationToken)) {
            return RequestAsync(method, path, new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"), cancellationToken);
        }

        /// <summary>
        /// Requests a serialized object with a JSON object response.
        /// </summary>
        /// <typeparam name="TReq">The request object type.</typeparam>
        /// <param name="method">The target method.</param>
        /// <param name="path">The target path.</param>
        /// <param name="body">The object to be serialized.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The JSON object response.</returns>
        protected internal async Task<JObject> RequestJsonSerializedObjectAsync<TReq>(HttpMethod method, string path, TReq body, CancellationToken cancellationToken = default(CancellationToken)) {
            HttpResponseMessage response = await RequestAsync(method, path, new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"), cancellationToken).ConfigureAwait(false);

            if (!response.Content.Headers.ContentType.MediaType.StartsWith("application/json", StringComparison.CurrentCultureIgnoreCase))
                throw new ApiException("Invalid server response", new ApiError[0], response);

            return JObject.Parse(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
        }

        /// <summary>
        /// Requests a JSON object with a serialized object response.
        /// </summary>
        /// <typeparam name="TRes">The response object type.</typeparam>
        /// <param name="method">The target method.</param>
        /// <param name="path">The target path.</param>
        /// <param name="body">The JSON body.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The response object.</returns>
        protected internal async Task<TRes> RequestJsonSerializedAsync<TRes>(HttpMethod method, string path, JObject body, CancellationToken cancellationToken = default(CancellationToken)) {
            HttpResponseMessage response = await RequestAsync(method, path, new StringContent(body.ToString(), Encoding.UTF8, "application/json"), cancellationToken).ConfigureAwait(false);

            if (!response.Content.Headers.ContentType.MediaType.StartsWith("application/json", StringComparison.CurrentCultureIgnoreCase))
                throw new ApiException("Invalid server response", new ApiError[0], response);

            return JsonConvert.DeserializeObject<TRes>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
        }

        /// <summary>
        /// Requests a serialized object with a serialized object response.
        /// </summary>
        /// <typeparam name="TReq">The request object type.</typeparam>
        /// <typeparam name="TRes">The response object type.</typeparam>
        /// <param name="method">The target method.</param>
        /// <param name="path">The target path.</param>
        /// <param name="body">The JSON body.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The response object.</returns>
        protected internal async Task<TRes> RequestJsonSerializedAsync<TReq, TRes>(HttpMethod method, string path, TReq body, CancellationToken cancellationToken = default(CancellationToken)) {
            string s = JsonConvert.SerializeObject(body);
            HttpResponseMessage response = await RequestAsync(method, path, new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"), cancellationToken).ConfigureAwait(false);
           
            if (!response.Content.Headers.ContentType.MediaType.StartsWith("application/json", StringComparison.CurrentCultureIgnoreCase))
                throw new ApiException("Invalid server response", new ApiError[0], response);

            return JsonConvert.DeserializeObject<TRes>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
        }

        /// <summary>
        /// Requests a serialized object response.
        /// </summary>
        /// <typeparam name="TRes">The response object type.</typeparam>
        /// <param name="method">The target method.</param>
        /// <param name="path">The target path.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The response object.</returns>
        protected internal async Task<TRes> RequestJsonSerializedAsync<TRes>(HttpMethod method, string path, CancellationToken cancellationToken = default(CancellationToken)) {
            HttpResponseMessage response = await RequestAsync(method, path, new StringContent("", Encoding.UTF8), cancellationToken).ConfigureAwait(false);

            if (!response.Content.Headers.ContentType.MediaType.StartsWith("application/json", StringComparison.CurrentCultureIgnoreCase))
                throw new ApiException("Invalid server response", new ApiError[0], response);

            return JsonConvert.DeserializeObject<TRes>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
        }
        #endregion

        #region API Methods
        /// <summary>
        /// Pings the API to check if it's up.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<string> PingAsync(CancellationToken cancellationToken = default(CancellationToken)) {
            return await (await RequestAsync(HttpMethod.Get, "ping", null, cancellationToken)).Content.ReadAsStringAsync();
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new WIFIPLUG client without a API key or secret.
        /// </summary>
        public ApiClient() : this(null) { }

        /// <summary>
        /// Create a new WIFIPLUG client.
        /// </summary>
        /// <param name="apiKey">The API key.</param>
        /// <param name="apiSecret">The API secret.</param>
        public ApiClient(string apiKey, string apiSecret) : this(API_URL) {
            if (apiKey == null)
                throw new ArgumentNullException(nameof(apiKey));
            else if (apiSecret == null)
                throw new ArgumentNullException(nameof(apiSecret));

            // set credentials for API
            _apiKey = apiKey;
            _apiSecret = apiSecret;

            // add key/secret headers
            _client.DefaultRequestHeaders.Add("X-API-Key", _apiKey);
            _client.DefaultRequestHeaders.Add("X-API-Secret", _apiSecret);
        }

        /// <summary>
        /// Creates a new WIFIPLUG client without a API key or secret.
        /// </summary>
        /// <param name="apiUrl">>The custom base path of the API.</param>
        public ApiClient(string apiUrl) {
            // setup client
            _client = new HttpClient();
            _client.BaseAddress = new Uri(apiUrl == null ? API_URL : apiUrl);
            _client.DefaultRequestHeaders.Add("X-API-Client", "api-client-net/1.0");

            // initialize operations
            Devices = new DeviceOperations(this);
            Users = new UserOperations(this);
            Sessions = new SessionOperations(this);
            Groups = new GroupOperations(this);
            Events = new EventOperations(this);
        }
        
        /// <summary>
        /// Create a new WIFIPLUG client.
        /// </summary>
        /// <param name="apiUrl">The custom base path of the API.</param>
        /// <param name="apiKey">The API key.</param>
        /// <param name="apiSecret">The API secret.</param>
        public ApiClient(string apiUrl, string apiKey, string apiSecret) : this(apiUrl) {
            if (apiKey == null)
                throw new ArgumentNullException(nameof(apiKey));
            else if (apiSecret == null)
                throw new ArgumentNullException(nameof(apiSecret));

            // set credentials for API
            _apiKey = apiKey;
            _apiSecret = apiSecret;

            // add key/secret headers
            _client.DefaultRequestHeaders.Add("X-API-Key", _apiKey);
            _client.DefaultRequestHeaders.Add("X-API-Secret", _apiSecret);
        }
        #endregion
    }
}
