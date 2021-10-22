# NamR

An app to keep a listof baby names and compare them with others (typically your significant other).

Built with Blazor Web Assembly.

Running on Heroku as a Docker Container using a Postgres database.

## Local setup

Make sure you have all required tools installed:

- Dotnet SDK version 5 or higher
- Docker

Set up a local database using docker:

```bash
docker run --name namrdb --restart on-failure -e POSTGRES_PASSWORD=supersecretpwd123 -e POSTGRES_USER=namr -p 5432:5432 -d postgres
```

Set the "Default" connection string as a user-secret like so:

```bash
cd ./src/NamR.Server
dotnet user-secrets set ConnectionStrings:Default "Host=localhost;Database=namr;Username=namr;Password=supersecretpwd"
```

Start the NamR.Server project

## Deployment

On each push to the `main` branch the app is packed into a Docker container and released to Heroku. You can see how that is set up in the [Dockerfile](./Dockerfile) and [workflow](./.github/workflows/buildanddeploy.yml) respectively.

## Quirks in the code due to the producion environment

Running .NET on Heroku is not straight forward, but fairly feasable through Docker. However, there are som quirks in the code to account for the Heroku environment.

### DATABASE_URL

A Heroku app with Postgres will make the connection information available to your app via the `DATABASE_URL` env variable. EF Core's Postgres connector has no idea what to do with this.

Enter these pieces of code that I mostly shamelessly copy/pasted from the internet.

1. The [PostgreSqlConnectionStringBuilder](./src/NamR.Server/PostgreSqlConnectionStringBuilder.cs)
2. This code in [Startup](./src/NamR.Server/Startup.cs) in the `ConfigureServices` method.

```C#
var dbUrl = Configuration["DATABASE_URL"];
if (dbUrl != null)
{
    var builder = new PostgreSqlConnectionStringBuilder(dbUrl)
    {
        Pooling = true,
        TrustServerCertificate = true,
        SslMode = SslMode.Require
    };

    services.AddDbContext<ListContext>(options => options.UseNpgsql(builder.ConnectionString));
}
else
{
    services.AddDbContext<ListContext>(options => options.UseNpgsql(Configuration.GetConnectionString("Default")));
}
```

### Listen on the right port

To make the app actually listen to the port it gets assigned by Heroku, two pieces of code are needed:

In the Dockerfile:

```Docker
ENV ASPNETCORE_URLS http://*:$PORT
```

In [Program](./src/NamR.Server/Program.cs):

```C#
var portEnv = Environment.GetEnvironmentVariable("PORT");
if (portEnv != null)
{
    webBuilder.ConfigureKestrel(serverOptions =>
    {
        serverOptions.Listen(IPAddress.Any, Convert.ToInt32(portEnv));
    });
}
```

### Https and 307 Internal Redirect

This code in the `Configure` method of [Startup](./src/NamR.Server/Startup.cs):

```C#
var forwardedHeadersOptions = new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
};
forwardedHeadersOptions.KnownNetworks.Clear();
forwardedHeadersOptions.KnownProxies.Clear();
app.UseForwardedHeaders(forwardedHeadersOptions);

var rewriteOptions = new RewriteOptions().AddRedirectToHttps(307);
app.UseRewriter(rewriteOptions);
```
