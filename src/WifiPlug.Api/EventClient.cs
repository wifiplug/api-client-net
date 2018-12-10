// Copyright (C) WIFIPLUG. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WifiPlug.Api.Scopes;
using WifiPlug.EventFramework;
using WifiPlug.EventFramework.Entities;

namespace WifiPlug.Api
{
    /// <summary>
    /// Provides access to the WIFIPLUG event subscription API.
    /// </summary>
    public class EventClient
    {
        #region Constants
        internal const string EventUri = "wss://event.wifiplug.co.uk/v1.0";
        #endregion

        #region Fields
        private ClientWebSocket _client = null;
        private Uri _uri = null;
        private IEventScope _scope = null;

        private SemaphoreSlim _subscriptionSemaphore = new SemaphoreSlim(1, 1);
        private List<string> _subscriptionList = new List<string>();

        private SemaphoreSlim _connectionSemaphore = new SemaphoreSlim(1, 1);
        private SemaphoreSlim _sendSemaphore = new SemaphoreSlim(1, 1);

        private bool _autoReconnect = true;
        private bool _reset = false;
        #endregion

        #region Events
        /// <summary>
        /// Called when the client connects.
        /// </summary>
        public event EventHandler<EventArgs> Connected;

        /// <summary>
        /// Called when the client disconnects.
        /// If auto reconnect is enabled this will still be called.
        /// </summary>
        public event EventHandler<EventArgs> Disconnected;

        /// <summary>
        /// Called when the client receives an event.
        /// </summary>
        public event EventHandler<EventReceivedEventArgs> Received;

        /// <summary>
        /// Called when the client receives an event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected void OnReceived(EventReceivedEventArgs e) {
            Received?.Invoke(this, e);
        }

        /// <summary>
        /// Called when the client connects.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected void OnConnected(EventArgs e) {
            Connected?.Invoke(this, e);
        }

        /// <summary>
        /// called when the client disconnects.
        /// </summary>
        /// <param name="e">The </param>
        protected void OnDisconnected(EventArgs e) {
            Disconnected?.Invoke(this, e);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets if the client should automatically attempt reconnect and resume previous subscriptions.
        /// </summary>
        public bool AutoReconnect {
            get {
                return _autoReconnect;
            } set {
                _autoReconnect = value;
            }
        }

        /// <summary>
        /// Gets the underlying WebSocket client.
        /// </summary>
        public ClientWebSocket Client {
            get {
                return _client;
            }
        }

        /// <summary>
        /// Gets or sets the authentication scope.
        /// This is only required if your API keys are not scoped to a user (personal API keys), you should most likely use <see cref="OAuth2Scope"/>.
        /// </summary>
        public IEventScope Scope {
            get {
                return _scope;
            } set {
                _scope = value;
            }
        }

        /// <summary>
        /// Gets all subscriptions.
        /// </summary>
        public IEnumerable<string> Subscriptions {
            get {
                lock(_subscriptionList)
                    return _subscriptionList.ToArray();
            }
        }
        #endregion

        #region Reading Loop
        /// <summary>
        /// The read loop.
        /// </summary>
        private async void ReadLoop() {
            do {
                if (_client.State == WebSocketState.Open) {
                    // create the receive buffer
                    ArraySegment<byte> receiveBuffer = new ArraySegment<byte>(new byte[8192]);
                    MemoryStream receiveStream = new MemoryStream();

                    while (_client.State == WebSocketState.Open) {
                        // receive into buffer
                        WebSocketReceiveResult result = null;

                        try {
                            result = await _client.ReceiveAsync(receiveBuffer, default(CancellationToken)).ConfigureAwait(false);
                        } catch (Exception ex) {
                            Debug.WriteLine($"Failed to receive buffer from websocket: {ex.ToString()}");
                            break;
                        }

                        // check if close message
                        if (result.MessageType == WebSocketMessageType.Close)
                            break;

                        // append to stream
                        receiveStream.Write(receiveBuffer.Array, receiveBuffer.Offset, result.Count);

                        // if end of message process
                        if (result.EndOfMessage) {
                            // get message data
                            byte[] messageBytes = receiveStream.ToArray();

                            // decode message
                            JObject message = null;

                            try {
                                message = JObject.Parse(Encoding.UTF8.GetString(messageBytes));
                            } catch (Exception ex) {
                                Debug.WriteLine($"Failed to parse buffer from websocket: {ex.ToString()}");
                                break;
                            }

                            // check message type
                            if (!message.TryGetValue("Type", out JToken typeToken) || typeToken.Type != JTokenType.String) {
                                Debug.WriteLine($"Failed to parse buffer from websocket: Type is empty or invalid");
                                break;
                            }

                            // check message payload
                            if (!message.TryGetValue("Data", out JToken payloadToken) || payloadToken.Type != JTokenType.Object) {
                                Debug.WriteLine($"Failed to parse buffer from websocket: Payload is empty or invalid");
                                break;
                            }

                            object data = null;
                            string type = ((string)typeToken);

                            if (type.Equals("Event", StringComparison.CurrentCultureIgnoreCase)) {
                                // deserialize event
                                try {
                                    data = payloadToken.ToObject<Event>();
                                } catch (Exception ex) {
                                    Debug.WriteLine($"Failed to parse deserialize event: {ex.ToString()}");
                                    break;
                                }

                                // deserialize event data
                                Event e = (Event)data;

                                try {
                                    e.Data = ((JObject)e.Data).ToObject(EventName.GetEntityType(((Event)data).Name));
                                } catch (Exception) {
                                    Debug.WriteLine($"Failed to parse buffer from websocket: Payload is empty or invalid");
                                }

                                // notify subscribers
                                OnReceived(new EventReceivedEventArgs(e, this));
                            } else {
                                Debug.WriteLine($"Unimplemented frame sent by server: {type}");
                            }

                            // prepare for next message
                            receiveStream = new MemoryStream();
                        }
                    }

                    // invoke disconnected
                    OnDisconnected(new EventArgs());
                }

                // try and auto reconnect
                try {
                    await ConnectAndAuthenticateAsync(default(CancellationToken), true).ConfigureAwait(false);
                    await ResubscribeAllAsync().ConfigureAwait(false);
                } catch {
                    await Task.Delay(5000).ConfigureAwait(false);
                }
            } while (_autoReconnect && !_reset);
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Resubscribes to all current subscriptions.
        /// </summary>
        /// <returns></returns>
        private async Task ResubscribeAllAsync() {
            await _sendSemaphore.WaitAsync().ConfigureAwait(false);

            try {
                // copy the subscriptions
                string[] subs = null;

                lock (_subscriptionList)
                    subs = _subscriptionList.ToArray();

                // subscribe to all
                foreach(string sub in _subscriptionList) {
                    string requestMsg = JsonConvert.SerializeObject(new Message() {
                        Payload = new SubscribeMessage() {
                            Selector = sub.ToString()
                        },
                        Type = "Subscribe",
                        MessageID = Guid.NewGuid()
                    });

                    // send to opposing client
                    await _client.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(requestMsg)), WebSocketMessageType.Text, true, default(CancellationToken))
                        .ConfigureAwait(false);
                }
            } finally {
                _sendSemaphore.Release();
            }
        }

        /// <summary>
        /// Subscribes to the provided selector.
        /// </summary>
        /// <param name="selector">The subscription selector.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        private async Task SubscribeNoConnectAsync(EventSelector selector, CancellationToken cancellationToken = default(CancellationToken)) {
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

            // wait for subscription semaphore
            await _subscriptionSemaphore.WaitAsync(cancellationToken).ConfigureAwait(false);
           
            try {
                // wait for send semaphore
                await _sendSemaphore.WaitAsync(cancellationToken).ConfigureAwait(false);

                try {
                    // send to opposing client
                    await _client.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(requestMsg)), WebSocketMessageType.Text, true, cancellationToken)
                        .ConfigureAwait(false);

                    // add to subscriptions
                    lock (_subscriptionList)
                        _subscriptionList.Add(selector.ToString());
                } finally {
                    _sendSemaphore.Release();
                }
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
        public async Task UnsubscribeNoConnectAsync(EventSelector selector, CancellationToken cancellationToken = default(CancellationToken)) {
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

            // wait for subscription semaphore
            await _subscriptionSemaphore.WaitAsync().ConfigureAwait(false);

            try {
                // wait for send semaphore
                await _sendSemaphore.WaitAsync(cancellationToken).ConfigureAwait(false);

                try {
                    // send to opposing client
                    await _client.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(requestMsg)), WebSocketMessageType.Text, true, cancellationToken).ConfigureAwait(false);

                    // remove from subscriptions
                    lock (_subscriptionList)
                        _subscriptionList.Remove(selector.ToString());
                } finally {
                    _sendSemaphore.Release();
                }
            } finally {
                _subscriptionSemaphore.Release();
            }
        }
        #endregion

        #region Methods
        private async Task ConnectAndAuthenticateAsync(CancellationToken cancellationToken, bool noReadLoop = false) {
            await _connectionSemaphore.WaitAsync().ConfigureAwait(false);

            try {
                if (_client != null && _client.State == WebSocketState.Open)
                    return;
                
                // build uri
                UriBuilder uriBuilder = new UriBuilder(_uri);

                // add scope
                if (_scope != null)
                    uriBuilder.Query = $"{uriBuilder.Query}&scope={WebUtility.HtmlEncode(_scope.ToString())}";

                // create client
                _client = new ClientWebSocket();

                await _client.ConnectAsync(uriBuilder.Uri, cancellationToken).ConfigureAwait(false);

                // start read loop
                if (!noReadLoop)
                    ReadLoop();

                // invoke on connected event
                OnConnected(new EventArgs());
            } finally {
                _connectionSemaphore.Release();
            }
        }

        /// <summary>
        /// Subscribes to the provided selector.
        /// </summary>
        /// <param name="selector">The subscription selector.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task SubscribeAsync(EventSelector selector, CancellationToken cancellationToken = default(CancellationToken)) {
            // connect if not open
            if (_client == null || _client.State != WebSocketState.Open) {
                _reset = false;
                await ConnectAndAuthenticateAsync(cancellationToken).ConfigureAwait(false);
            }

            await SubscribeNoConnectAsync(selector, cancellationToken).ConfigureAwait(false);
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
            if (_client == null || _client.State != WebSocketState.Open) {
                _reset = false;
                await ConnectAndAuthenticateAsync(cancellationToken).ConfigureAwait(false);
            }

            await UnsubscribeNoConnectAsync(selector, cancellationToken).ConfigureAwait(false);
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
            // set reset to true, stopping the auto reconnect routine
            _reset = true;

            // if already closed or unconnected don't bother
            if (_client == null || _client.State == WebSocketState.Closed)
                return Task.FromResult(true);

            return _client.CloseAsync(WebSocketCloseStatus.NormalClosure, "Client requested closure", cancellationToken);
        }
        #endregion


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
            // build uri
            UriBuilder uriBuilder = new UriBuilder(eventUrl);
            uriBuilder.Query = $"key={WebUtility.UrlEncode(apiKey)}&secret={WebUtility.UrlEncode(apiSecret)}";

            _uri = uriBuilder.Uri;
        }
        #endregion
    }

    /// <summary>
    /// Represents event arguments where the client receives an event.
    /// </summary>
    public class EventReceivedEventArgs
    {
        /// <summary>
        /// Gets the event.
        /// </summary>
        public Event Event { get; private set; }

        internal EventReceivedEventArgs(Event e, EventClient client) {
            Event = e;
        }
    }
}