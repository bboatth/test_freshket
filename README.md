# Food Store

Requires the .NET 8 SDK. Check installed SDKs:

```powershell
dotnet --list-sdks
```

If no `8.0.x` SDK is listed or `dotnet` is not recognized, install it on Windows:

```powershell
winget install --id Microsoft.DotNet.SDK.8 --exact
```

Or download the SDK from [Microsoft](https://dotnet.microsoft.com/en-us/download/dotnet/8.0). Reopen your terminal after installation.

Run from the project folder:

```powershell
dotnet run --project test_market.csproj
```
