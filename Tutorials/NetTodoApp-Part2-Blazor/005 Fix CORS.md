# Adding access to the Web API

Currently, your Web API does not have "Cross Origin Resource Sharing" enabled, which prevents your Blazor app from accessing the API.

In your WebAPI/Program.cs add the following:

```csharp
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials());
```

It must be after the app variable is defined, [see here](https://github.com/TroelsMortensen/WasmTodo/blob/010_AddUser/WebAPI/Program.cs), probably lines 26-30.

If you wish to know more about CORS, [read here](https://developer.mozilla.org/en-US/docs/Web/HTTP/CORS). For this course, it is just something we enable, without caring too much about it. It is a security measure, which we are disabling.

## Setting base address of HttpClient
If we open BlazorWASM/Program.cs, we will find that an HttpClient is added as a scoped service, and the base address is set to be the address of the host environment, i.e. the localhost.

The host address is that of your Blazor WASM app. However, we need to contact the Web API, which has a different address.\
Run your Web API to see in the console which https address it is listening on. Or look in the launchSettings.json, mentioned on slide 1.

Copy this address into the object initializer, like this:

```csharp{4}
builder.Services.AddScoped(
    sp => 
        new HttpClient { 
            BaseAddress = new Uri("https://localhost:7093") 
        }
);
```
