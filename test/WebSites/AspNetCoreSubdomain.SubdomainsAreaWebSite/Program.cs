using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace AspNetCoreSubdomain.SubdomainsAreaWebSite
{
    public class Program
    {
        public static void Main(string[] args)
        {
             var host = new WebHostBuilder()
                 .ConfigureLogging(logging =>
                 {
                     logging.ClearProviders();
                     logging.AddDebug();
                 })
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .UseKestrel()
                .UseIISIntegration()
                .Build();

            host.Run();
        }
    }
}
