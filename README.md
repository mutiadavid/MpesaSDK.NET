# MpesaSDK.NET
Mpesa Daraja SDK implementation for .net.

[![Build Status](https://img.shields.io/endpoint.svg?url=https%3A%2F%2Factions-badge.atrox.dev%2Fmutiadavid%2FMpesaSDK.NET%2Fbadge%3Fref%3Dmaster&style=for-the-badge)](https://actions-badge.atrox.dev/mutiadavid/MpesaSDK.NET/goto?ref=master) ![Nuget](https://img.shields.io/nuget/v/MpesaSDK.NET?style=for-the-badge) ![Nuget](https://img.shields.io/nuget/dt/MpesaSDK.NET?style=for-the-badge) 


For More Info check [Safaricom Mpesa Documentation](https://developer.safaricom.co.ke/docs#authentication)

# Download and Install
nugget [install](https://www.nuget.org/packages/MpesaSDK.NET/)

# Usage

### Sending Requests
***

##### Mpesa Client
```cs

using MpesaSDK.NET.Dtos.InitiateRequests;
using MpesaSDK.NET.Dtos.Responses;
using MpesaSDK.NET.Enums;
...
//http client
 using HttpClient client = new() { Timeout = TimeSpan.FromSeconds(10) };
//mpesa client
 MpesaClient mpesaClient = new MpesaClient(client, new MpesaClientOptions
 {
     ConsumerKey = "your-consumerkey",
     ConsumerSecret = "your-consumer-secret",
     IsSandBox = true /* change this flag to false if Production */ 
 });
```

##### STK Push request
```cs

var result = await mpesaclient.STKPushAsync(...);

```

##### Stk Push Query request
```cs

var result = await mpesaclient.StkPushQueryAsync(...);

```

##### B2C request
```cs

var result = await mpesaclient.B2CAsync(...);

```

##### B2B request
```cs

var result = await mpesaclient.B2BAsync(...);

```
>To get MPesa Security credetials for both B2B and B2C use method below. 

```cs 
string credential = "pass".ToMpesaSecurityCredential(); 
```

##### C2B Register Url request
```cs

var result = await mpesaclient.C2BRegisterUrlAsync(...);

```

##### C2B Simulate Transaction request
```cs

var result = await mpesaclient.C2BSimulateTransactionAsync(...);

```

##### Account Balance request
```cs

var result = await mpesaclient.AccountBalanceAsync(...);

```

##### Transaction Status request
```cs

var result = await mpesaclient.TransactionStatusAsync(...);

```

##### Reversal request
```cs

var result = await mpesaclient.ReversalAsync(...);

```

### Callback server

***
Check sample callback api [MpesaSDK.NET.CallbackAPI](https://github.com/mutiadavid/MpesaSDK.NET/tree/master/MpesaSDK.NET.CallbackAPI)



Docs to be updated soon.


## LICENSE

Apache LICENSE-2.0 [read more](https://www.apache.org/licenses/LICENSE-2.0)


