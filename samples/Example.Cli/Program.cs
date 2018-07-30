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
            cc.Authentication = new SessionAuthentication(Environment.GetEnvironmentVariable("SESSION_TOKEN"));

            DeviceSetupTokenEntity entity = await cc.Devices.AddDeviceSetupTokenAsync();
        }
    }
}
