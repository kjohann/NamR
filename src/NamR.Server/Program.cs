using System;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace NamR.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    var portEnv = Environment.GetEnvironmentVariable("PORT");
                    if (portEnv != null)
                    {
                        webBuilder.ConfigureKestrel(serverOptions =>
                        {
                            serverOptions.Listen(IPAddress.Any, Convert.ToInt32(portEnv));
                        });
                    }
                    
                    webBuilder.UseStartup<Startup>();
                });
    }
}
