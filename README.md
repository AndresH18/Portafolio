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
app.MapFallbackToFile("/webassembly/{*path:nonfile}", "/webassembly/index.html"); 
...
```
This will enable blazor web assembly in the host.  
The `app.MapFallbackToFile` maps a route template (in this case "webassembly/*") to the webassembly app index file (in this case, the index.html starts with "webassembly" because that is the string we decided to use).