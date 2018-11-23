using System;
using System.Threading.Tasks;
using WifiPlug.Api;

namespace Example.EventCli
{
    class Program
    {
        static void Main(string[] args) => MainAsync(args).Wait();

        static async Task MainAsync(string[] args) {
            EventClient eventClient = new EventClient("ws://localhost/v1.0", "devkey", "devsecret");
            await eventClient.SubscribeAsync("device:*/*");

            await Task.Delay(3000000);
        }
    }
}
