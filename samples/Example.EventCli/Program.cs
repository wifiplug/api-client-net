// Copyright (C) WIFIPLUG. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using System.Threading.Tasks;
using WifiPlug.Api;

namespace Example.EventCli
{
    class Program
    {
        static void Main(string[] args) => MainAsync(args).Wait();

        static async Task MainAsync(string[] args) {
            EventClient eventClient = new EventClient("ws://localhost/v1.0", Environment.GetEnvironmentVariable("API_KEY"), Environment.GetEnvironmentVariable("API_SECRET"));
            await eventClient.SubscribeAsync("device:*/*");

            await Task.Delay(3000000);
        }
    }
}
