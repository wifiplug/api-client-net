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
        public DeviceOperations Devices { get; private set; }

        /// <summary>
        /// Gets the session operations.
        /// </summary>
        public SessionOperations Sessions { get; private set; }

        /// <summary>
        /// Gets the user operations.
        /// </summary>
        public UserOperations Users { get; private set; }

        /// <summary>
        /// Gets the group operations.
        /// </summary>
        public GroupOperations Groups { get; private set; }
        #endregion

        #region Rest Methods
        internal async Task<HttpResponseMessage> RequestAsync(HttpMethod method, string path, HttpContent content, CancellationToken cancellationToken = default(CancellationToken)) {
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

        internal async Task<HttpResponseMessage> RawRequestAsync(HttpMethod method, string path, HttpContent content, CancellationToken cancellationToken = default(CancellationToken)) {
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
                    JObject obj = JObject.Parse(await response.Content.ReadAsStringAsync().ConfigureAwait(false));

                    JArray errJson = (JArray)obj["errors"];
                    List<ApiError> errList = new List<ApiError>(errJson.Count);
                    
                    foreach(JObject err in errJson) {
                        errList.Add(new ApiError((string)err["error"], (string)err["message"]));
                    }

                    errArr = errList.ToArray();
                    errMessage = (errArr.Length > 1 ? $"{errList.Count} errors occured" : errArr[0].Message) ?? "Unspecified error";
                } catch(Exception) {
                    throw new ApiException(string.Format("Invalid server response - {0} {1}", (int)response.StatusCode, response.StatusCode), new ApiError[0], response);
                }

                // throw api exception
                throw new ApiException(errMessage, errArr, response);
            }
        }

        internal async Task<string> RequestStringAsync(HttpMethod method, string path, HttpContent body = null, CancellationToken cancellationToken = default(CancellationToken)) {
            return await (
                await RequestAsync(method, path, body, cancellationToken).ConfigureAwait(false))
                .Content.ReadAsStringAsync().ConfigureAwait(false);
        }

        internal async Task<JObject> RequestJsonObjectAsync(HttpMethod method, string path, HttpContent body = null, CancellationToken cancellationToken = default(CancellationToken))  {
            HttpResponseMessage response = await RequestAsync(method, path, body, cancellationToken).ConfigureAwait(false);

            if (!response.Content.Headers.ContentType.MediaType.StartsWith("application/json", StringComparison.CurrentCultureIgnoreCase))
                throw new ApiException("Invalid server response", new ApiError[0], response);

            return JObject.Parse(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
        }

        internal Task RequestJsonAsync(HttpMethod method, string path, HttpContent body = null, CancellationToken cancellationToken = default(CancellationToken)) {
            return RequestAsync(method, path, body, cancellationToken);
        }

        internal Task RequestJsonAsync(HttpMethod method, string path, JObject body, CancellationToken cancellationToken = default(CancellationToken)) {
            return RequestAsync(method, path, new StringContent(body.ToString(), Encoding.UTF8, "application/json"), cancellationToken);
        }

        internal Task<JObject> RequestJsonObjectAsync(HttpMethod method, string path, JObject body, CancellationToken cancellationToken = default(CancellationToken)) {
            return RequestJsonObjectAsync(method, path, new StringContent(body.ToString(), Encoding.UTF8, "application/json"));
        }

        internal Task RequestJsonSerializedAsync<TReq>(HttpMethod method, string path, TReq body, CancellationToken cancellationToken = default(CancellationToken)) {
            return RequestAsync(method, path, new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"), cancellationToken);
        }

        internal async Task<JObject> RequestJsonSerializedObjectAsync<TReq>(HttpMethod method, string path, TReq body, CancellationToken cancellationToken = default(CancellationToken)) {
            HttpResponseMessage response = await RequestAsync(method, path, new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"), cancellationToken).ConfigureAwait(false);

            if (!response.Content.Headers.ContentType.MediaType.StartsWith("application/json", StringComparison.CurrentCultureIgnoreCase))
                throw new ApiException("Invalid server response", new ApiError[0], response);

            return JObject.Parse(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
        }

        internal async Task<TRes> RequestJsonSerializedAsync<TRes>(HttpMethod method, string path, JObject body, CancellationToken cancellationToken = default(CancellationToken)) {
            HttpResponseMessage response = await RequestAsync(method, path, new StringContent(body.ToString(), Encoding.UTF8, "application/json"), cancellationToken).ConfigureAwait(false);

            if (!response.Content.Headers.ContentType.MediaType.StartsWith("application/json", StringComparison.CurrentCultureIgnoreCase))
                throw new ApiException("Invalid server response", new ApiError[0], response);

            return JsonConvert.DeserializeObject<TRes>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
        }

        internal async Task<TRes> RequestJsonSerializedAsync<TReq, TRes>(HttpMethod method, string path, TReq body, CancellationToken cancellationToken = default(CancellationToken)) {
            string s = JsonConvert.SerializeObject(body);
            HttpResponseMessage response = await RequestAsync(method, path, new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"), cancellationToken).ConfigureAwait(false);
           
            if (!response.Content.Headers.ContentType.MediaType.StartsWith("application/json", StringComparison.CurrentCultureIgnoreCase))
                throw new ApiException("Invalid server response", new ApiError[0], response);

            return JsonConvert.DeserializeObject<TRes>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
        }

        internal async Task<TRes> RequestJsonSerializedAsync<TRes>(HttpMethod method, string path, CancellationToken cancellationToken = default(CancellationToken)) {
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
        public ApiClient() {
            // setup client
            _client = new HttpClient();
            _client.BaseAddress = new Uri(API_URL);
            _client.DefaultRequestHeaders.Add("X-API-Client", "api-client-net/1.0");
            
            // initialize operations
            Devices = new DeviceOperations(this);
            Users = new UserOperations(this);
            Sessions = new SessionOperations(this);
            Groups = new GroupOperations(this);
        }

        /// <summary>
        /// Create a new WIFIPLUG client.
        /// </summary>
        /// <param name="apiKey">The API key.</param>
        /// <param name="apiSecret">The API secret.</param>
        public ApiClient(string apiKey, string apiSecret) :this() {
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
