# How to add Blazor Webassemly in ASP.Net Core Web App

The host project must have the `microsoft.aspnetcore.components.webassembly.server` nuget installed.

### WASM project

- In `<Project-name>.csproj` add `<StaticWebAssetBasePath>/webassembly/</StaticWebAssetBasePath>`, with **webassembly** being 
the webassembly route root. ***It must start and end with the forward slash.*** This string will be used a 
lot, so don't forget which one you decide to use.  
_It may seem that the starting and ending slash is not necessary here._

```xml
  <PropertyGroup>
    <TargetFramework>net7.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
    <StaticWebAssetBasePath>/webassembly/</StaticWebAssetBasePath>
  </PropertyGroup>
``` 

- In `wwwroot\index.html` find the tag `<base href="/" />`. You must replace the content of the `href` attribute with the same value used in the previous step, in this case `/webassembly/`.  
**IT MUST START AND END WITH FORWARD SLASH**.
```html
<head>
    ...
    <title>title</title>
    <base href="/webassembly/" />
</head>
```

### Host project

In this repo, this project is called `Advanced`.

- In `Program.cs`, add the following. You must use the same name as before.  
```csharp
...
// This

app.UseBlazorFrameworkFiles("/webassembly"); // must start with forward slash, but seems it doesn't require the ending one
app.UseStaticFiles(); // allow delivery of static files. Will allow sending the necesary files for wasm
app.MapFallbackToFile("/webassembly/{*path:nonfile}", "/webassembly/index.html"); 
...
```
This will enable blazor web assembly in the host.  
The `app.MapFallbackToFile` maps a route template (in this case "webassembly/*") to the webassembly app index file (in this case, the index.html starts with "webassembly" because that is the string we decided to use).

> [!IMPORTANT]
> The `<StaticWebAssetBasePath>` is used when compiling the WASM project as the base path for all the static resources created. This means that all files would be relative to that value.  
>
> Setting `<base href="/webassembly/" />` will set the relative location of all resources used by the WASM when running. This means that the Blazor WebAssembly will look inside of "webassembly" for instead of "/" when searching for files it needs. The absence of this is considered as "/".
>
> We could think of `<StaticWebAssetBasePath>` as setting relative location of files on compilation, and `<base href="/webassembly/" />` as specifying to Blazor WebAssembly the relative location of its files at runtime. 

> [!CAUTION]
> It is important to always make sure `<StaticWebAssetBasePath>` and `<base href />` are the same. If they are different the application will **NOT** work correctly.
