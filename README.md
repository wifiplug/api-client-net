<div align="center">

[![GitHub license](https://img.shields.io/badge/license-Apache%202-blue.svg?style=flat-square)](https://raw.githubusercontent.com/wifiplug/api-client-net/master/README.md)
[![GitHub issues](https://img.shields.io/github/issues/wifiplug/api-client-net.svg?style=flat-square)](https://github.com/wifiplug/api-client-net/issues)
[![GitHub stars](https://img.shields.io/github/stars/wifiplug/api-client-net.svg?style=flat-square)](https://github.com/wifiplug/api-client-net/stargazers)
[![GitHub forks](https://img.shields.io/github/forks/wifiplug/api-client-net.svg?style=flat-square)](https://github.com/wifiplug/api-client-net/network)

</div>

# WifiPlug API

The .NET API client for the WIFIPLUG platform.

## Get Started

[![NuGet Status](https://img.shields.io/nuget/v/WifiPlug.Api.svg?style=flat)](https://www.nuget.org/packages/WifiPlug.Api/)

You can install the package using either the CLI:

```
dotnet add package WifiPlug.Api
```

or from the NuGet package manager:

```
Install-Package WifiPlug.Api
```

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

## Contributing

We welcome any pull requests or bug reports, please try and keep to the existing style conventions and comment any additions. The issues section is only for problems related to the API client, other issues will be closed.