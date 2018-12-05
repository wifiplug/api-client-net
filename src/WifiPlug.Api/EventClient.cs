// Copyright (C) WIFIPLUG. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WifiPlug.Api
{
    /// <summary>
    /// Provides access to the WIFIPLUG event subscription API.
    /// </summary>
    public class EventClient : IObservable<Event>
    {
        #region Constants
        internal const string EventUri = "wss://event.wifiplug.co.uk/v1.0";
        #endregion

        #region Fields
        private ClientWebSocket _client = null;
        private Uri _uri = null;

        private SemaphoreSlim _subscriptionSemaphore = new SemaphoreSlim(1, 1);
        private List<string> _subscriptionList = new List<string>();
        #endregion

        #region Methods
        /// <summary>
        /// Subscribes to the provided selector.
        /// </summary>
        /// <param name="selector">The subscription selector.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task SubscribeAsync(EventSelector selector, CancellationToken cancellationToken = default(CancellationToken)) {
            // connect if not open
            if (_client.State != WebSocketState.Open)
                await _client.ConnectAsync(_uri, cancellationToken).ConfigureAwait(false);

            // throw if cancelled
            cancellationToken.ThrowIfCancellationRequested();

            // serialize
            string requestMsg = JsonConvert.SerializeObject(new Message() {
                Payload = new SubscribeMessage() {
                    Selector = selector.ToString()
                },
                Type = "Subscribe",
                MessageID = Guid.NewGuid()
            });

            // subscribe
            await _subscriptionSemaphore.WaitAsync().ConfigureAwait(false);

            try {
                await _client.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(requestMsg)), WebSocketMessageType.Text, true, cancellationToken).ConfigureAwait(false);

                lock(_subscriptionList)
                _subscriptionList.Add(selector.ToString());
            } finally {
                _subscriptionSemaphore.Release();
            }
        }

        /// <summary>
        /// Subscribes to the provided selector.
        /// </summary>
        /// <param name="selector">The subscription selector.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <example>await SubscribeAsync("device/*/*);</example>
        /// <returns></returns>
        public Task SubscribeAsync(string selector, CancellationToken cancellationToken = default(CancellationToken)) {
            return SubscribeAsync(new EventSelector(selector), cancellationToken);
        }

        /// <summary>
        /// Unsubscribes the provided selector, you can only unsubscribe the exact selector you subscribed previously.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task UnsubscribeAsync(EventSelector selector, CancellationToken cancellationToken = default(CancellationToken)) {
            // connect if not open
            if (_client.State != WebSocketState.Open)
                await _client.ConnectAsync(_uri, cancellationToken).ConfigureAwait(false);

            // throw if cancelled
            cancellationToken.ThrowIfCancellationRequested();

            // serialize
            string requestMsg = JsonConvert.SerializeObject(new Message() {
                Payload = new UnsubscribeMessage() {
                    Selector = selector.ToString()
                },
                Type = "Unsubscribe",
                MessageID = Guid.NewGuid()
            });

            // unsubscribe
            await _subscriptionSemaphore.WaitAsync().ConfigureAwait(false);

            try {
                await _client.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(requestMsg)), WebSocketMessageType.Text, true, cancellationToken).ConfigureAwait(false);
            } finally {
                _subscriptionSemaphore.Release();
            }
        }

        /// <summary>
        /// Unsubscribes the provided selector, you can only unsubscribe the exact selector you subscribed previously.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task UnsubscribeAsync(string selector, CancellationToken cancellationToken = default(CancellationToken)) {
            return UnsubscribeAsync(new EventSelector(selector), cancellationToken);
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

        #region Classes
        class Message
        {
            public string Type { get; set; }
            public object Payload { get; set; }
            public Guid MessageID { get; set; }
        }

        class SubscribeMessage
        {
            public string Selector { get; set; }
        }

        class UnsubscribeMessage
        {
            public string Selector { get; set; }
        }

        class EventMessage
        {
            Guid UUID { get; set; }
            string Name { get; set; }
            string Resource { get; set; }
            string ResourceType { get; set; }
            DateTime Timestamp { get; set; }
            JObject Payload { get; set; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new event client and configures the provided event URL and api credentials.
        /// </summary>
        /// <remarks>Your API credentials must allow event streaming.</remarks>
        /// <param name="apiKey">The API key.</param>
        /// <param name="apiSecret">The API secret.</param>
        public EventClient(string apiKey, string apiSecret) 
            : this(EventUri, apiKey, apiSecret){
        }

        /// <summary>
        /// Creates a new event client and configures the provided event URL and api credentials.
        /// </summary>
        /// <remarks>Your API credentials must allow event streaming.</remarks>
        /// <param name="eventUrl">The event URL.</param>
        /// <param name="apiKey">The API key.</param>
        /// <param name="apiSecret">The API secret.</param>
        public EventClient(string eventUrl, string apiKey, string apiSecret) {
            // create client
            _client = new ClientWebSocket();

            // build uri
            UriBuilder uriBuilder = new UriBuilder(eventUrl);
            uriBuilder.Query = $"key={WebUtility.UrlEncode(apiKey)}&secret={WebUtility.UrlEncode(apiSecret)}";

            _uri = uriBuilder.Uri;
        }
        #endregion
    }
}