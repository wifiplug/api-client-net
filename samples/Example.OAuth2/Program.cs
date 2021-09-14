using System;
using System.Threading;
using System.Threading.Tasks;
using WifiPlug.Api;
using WifiPlug.Api.Authentication;
using WifiPlug.Api.Entities;

namespace Example.OAuth2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // API Key and Secret
            string apiKey = Environment.GetEnvironmentVariable("API_KEY");
            string apiSecret = Environment.GetEnvironmentVariable("API_SECRET");

            // OAuth2 ID and Secret
            string clientID = Environment.GetEnvironmentVariable("OAUTH2_CLIENT_ID");
            string clientSecret = Environment.GetEnvironmentVariable("OAUTH2_CLIENT_SECRET");

            // Create an API client
            ApiClient client = new ApiClient(apiKey, apiSecret);

            // Exchange the authorization_code for the access and refresh tokens
            try
            {
                // Rebuild the authentication
                client.Authentication = new OAuth2Authentication(clientID, clientSecret, "access_token", "refresh_token");

                // Make another request.
                var devices = await client.Devices.ListDevicesAsync();

                await Task.Delay(Timeout.Infinite);
            } catch (ApiException ex)
            {
                // A failure to authenticate will be thrown with this exception
                throw;
            }
        }
    }
}
