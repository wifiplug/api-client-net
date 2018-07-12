# WifiPlug.Api

The .NET API client for the WIFIPLUG platform.

## Example

An example of using the API asynchronously.

```csharp
using System;
using System.Threading.Tasks;
using WifiPlug.Api;
using WifiPlug.Api.Entities;
using WifiPlug.Api.Authentication;

class Program
{
    static void Main(string[] args) => AsyncMain(args).Wait();

    static async Task AsyncMain(string[] args) {
        // create the client
        ApiClient client = new ApiClient("your api key", "your api secret");
		
	// You must also add some form of authentication. Session authorisation is
	// internal so you can define your own by inheriting ApiAuthentication.
	// client.Authentication = new SessionAuthentication("token");

        // request my user object
        UserEntity user = await client.Users.GetCurrentUserAsync();

        Console.WriteLine($"Hello {user.GivenName} {user.FamilyName}!");
    }
}
```
