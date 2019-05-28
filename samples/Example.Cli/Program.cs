// Copyright (C) WIFIPLUG. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WifiPlug.Api;
using WifiPlug.Api.Authentication;
using WifiPlug.Api.Entities;
using WifiPlug.Api.Operations;
using WifiPlug.Api.Schema;

namespace Example.Cli
{
    class Program
    {
        static void Main(string[] args) => AsyncMain(args).Wait();

        static async Task AsyncMain(string[] args) {
            ApiClient cc = new ApiClient(Environment.GetEnvironmentVariable("API_KEY"), Environment.GetEnvironmentVariable("API_SECRET"));

            cc.Authentication = new BearerAuthentication("BEARER TOKEN");

            var device = await cc.Devices.GetDeviceAsync(new Guid("a765fb47-f2f5-40c9-bfb8-b63a8515adfd"));

            await cc.Devices.EditDeviceServiceCharacteristicAsync(new Guid("a765fb47-f2f5-40c9-bfb8-b63a8515adfd"), new Guid("8677ecf6-7fd4-4144-8a6d-b3eef7efa00a"), new Guid("6862bf69-1e2a-4e0b-b27a-afc8c498c806"), true);
        }
    }
}
