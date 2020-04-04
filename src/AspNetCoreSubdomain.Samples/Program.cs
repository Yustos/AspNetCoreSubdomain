using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net;

namespace AspNetCoreSubdomain.Samples
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureLogging(logging =>
				{
					logging.SetMinimumLevel(LogLevel.Trace);
					logging.ClearProviders();
					logging.AddDebug();
					logging.AddConsole();
				})
			
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder
						//.UseHttpSys(options =>
						//{
						//	options.Authentication.Schemes =
						//		AuthenticationSchemes.NTLM |
						//		AuthenticationSchemes.Negotiate;
						//	options.Authentication.AllowAnonymous = false;
						//})
						.ConfigureKestrel(serverOptions =>
						{
							serverOptions.Listen(IPAddress.Any, 5000, listenOptions =>
							{
								listenOptions.UseConnectionLogging();
							});
						})
						.UseStartup<Startup>();
				});
	}
}
