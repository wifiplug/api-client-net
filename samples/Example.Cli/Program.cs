using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WifiPlug.Api;
using WifiPlug.Api.Authentication;
using WifiPlug.Api.Entities;
using WifiPlug.Api.Schema;

namespace Example.Cli
{
    class Program
    {
        static void Main(string[] args) => AsyncMain(args).Wait();

        static async Task AsyncMain(string[] args) {
            ApiClient cc = new ApiClient(Environment.GetEnvironmentVariable("API_KEY"), Environment.GetEnvironmentVariable("API_SECRET"));
            cc.Authentication = new SessionAuthentication(Environment.GetEnvironmentVariable("SESSION_TOKEN"));
            
            var q = await cc.Groups.ListGroupsAsync();
            var i = await cc.Groups.ListGroupItemsAsync(q[1].UUID);
        }
    }
}
