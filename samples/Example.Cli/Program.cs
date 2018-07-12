using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WifiPlug.Api;
using WifiPlug.Api.Authentication;
using WifiPlug.Api.Entities;

namespace Example.Cli
{
    class Program
    {
        static void Main(string[] args) => AsyncMain(args).Wait();

        static async Task AsyncMain(string[] args) {
            ApiClient client = new ApiClient(Environment.GetEnvironmentVariable("API_KEY"), Environment.GetEnvironmentVariable("API_SECRET"));
            client.Authentication = new SessionAuthentication(Environment.GetEnvironmentVariable("SESSION_TOKEN"));

            var l = await client.Devices.ListDevicesAsync();

            foreach(var device in l) {
                Console.WriteLine($"Device: {device.Name} IsOnline: {device.IsOnline}");
            }
        }
    }
}
