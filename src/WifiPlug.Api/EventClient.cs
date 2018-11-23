using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WifiPlug.Api
{
    /// <summary>
    /// Provides access to the WIFIPLUG event subscription API.
    /// </summary>
    class EventClient : IObservable<Event>
    {
        #region Constants
        internal const string API_URL = "wss://event.wifiplug.co.uk/v1.0";
        #endregion

        #region Fields
        private string _apiKey = null;
        private string _apiSecret = null;
        private ClientWebSocket _client = null;
        private Uri _uri = null;
        #endregion

        #region Methods
        /// <summary>
        /// Subscribes to the provided selector.
        /// </summary>
        /// <param name="selector">The subscription selector.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <example>await SubscribeAsync("device/*/*);</example>
        /// <returns></returns>
        public async Task SubscribeAsync(string selector, CancellationToken cancellationToken = default(CancellationToken)) {
            // connect if not open
            if (_client.State != WebSocketState.Open)
                await _client.ConnectAsync(_uri, cancellationToken).ConfigureAwait(false);

            // throw if cancelled
            cancellationToken.ThrowIfCancellationRequested();

            // subscribe
            //_client.SendAsync(new ArraySegment<byte>());
        }

        /// <summary>
        /// Unsubscribes the provided selector, you can only unsubscribe the exact selector you subscribed previously.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task UnsubscribeAsync(string selector, CancellationToken cancellationToken = default(CancellationToken)) {
            // connect if not open
            if (_client.State != WebSocketState.Open)
                await _client.ConnectAsync(_uri, cancellationToken).ConfigureAwait(false);

            // throw if cancelled
            cancellationToken.ThrowIfCancellationRequested();

            // unsubscribe
        }

        /// <summary>
        /// Closes the underlying event streaming client gracefully.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task CloseAsync(CancellationToken cancellationToken = default(CancellationToken)) {
            return _client.CloseAsync(WebSocketCloseStatus.NormalClosure, "Client requested closure", cancellationToken);
        }
        #endregion

        public IDisposable Subscribe(IObserver<Event> observer) {
            throw new NotImplementedException();
        }

        #region Constructors
        /// <summary>
        /// Creates a new event client and configures the provided event URL and api credentials.
        /// </summary>
        /// <remarks>Your API credentials must allow event streaming.</remarks>
        /// <param name="apiKey">The API key.</param>
        /// <param name="apiSecret">The API secret.</param>
        public EventClient(string apiKey, string apiSecret) 
            : this(null, apiKey, apiSecret){
        }

        /// <summary>
        /// Creates a new event client and configures the provided event URL and api credentials.
        /// </summary>
        /// <remarks>Your API credentials must allow event streaming.</remarks>
        /// <param name="eventUrl">The event URL.</param>
        /// <param name="apiKey">The API key.</param>
        /// <param name="apiSecret">The API secret.</param>
        public EventClient(string eventUrl, string apiKey, string apiSecret) {
            _client = new ClientWebSocket();
        }
        #endregion
    }
}