// Copyright (C) WIFIPLUG. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using WifiPlug.Api;
using WifiPlug.Api.Scopes;

namespace Example.EventCli
{
    class Program
    {
        static async Task Main(string[] args) {
            EventClient eventClient = new EventClient("wss://event.wifiplug.co.uk/v1.0", Environment.GetEnvironmentVariable("API_KEY"), Environment.GetEnvironmentVariable("API_SECRET"));
            eventClient.Scope = new SessionScope(Environment.GetEnvironmentVariable("SCOPE_SESSION"));

            eventClient.Received += (o, e) => {
                Console.WriteLine(JsonConvert.SerializeObject(e.Event, Formatting.Indented));

                if (e.Event.Data is WifiPlug.EventFramework.Entities.DeviceCharacteristicChangeEntity) {
                    var ev = e.Event.Data as WifiPlug.EventFramework.Entities.DeviceCharacteristicChangeEntity;

                    Console.WriteLine($"Characteristic change: {ev.OldValue} -> {ev.NewValue}");
                } else if (e.Event.Data is WifiPlug.EventFramework.Entities.DeviceStatusEntity) {
                    var ev = e.Event.Data as WifiPlug.EventFramework.Entities.DeviceStatusEntity;

                    Console.WriteLine($"Status: {ev.OldState} -> {ev.NewState}");
                } else {
                    Console.WriteLine($"Received event {e.Event.Name}");
                }
            };
            eventClient.Connected += (o, e) => Console.WriteLine($"Connected");
            eventClient.Disconnected += (o, e) => Console.WriteLine($"Disconnected");

            await eventClient.SubscribeAsync("device:*.*");

            await Task.Delay(3000000);
        }
    }
}
