﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AspNetCoreSubdomain.Samples
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddSubdomains();
            services.AddMvc(options => options.EnableEndpointRouting = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                var hostnames = new[] { "localhost:54575" };

                routes.MapSubdomainRoute(
                    hostnames,
                    "StaticSubdomain1",
                    "staticSubdomain1",
                    "{controller}/{action}/{parameter}/");

                routes.MapSubdomainRoute(
                    hostnames,
                    "StaticSubdomain2",
                    "staticSubdomain2",
                    "test",
                    new { controller = "Home", action = "Action1" });

                routes.MapSubdomainRoute(
                    hostnames,
                    "SubdomainsPage",
                    "subdomains.page",
                    "",
                    new { controller = "Home", action = "SubdomainsPage" });

                routes.MapSubdomainRoute(
                    hostnames,
                    "SubdomainFormsPage",
                    "subdomain.forms.page",
                    "",
                    new { controller = "Home", action = "SubdomainFormsPage" });

                routes.MapSubdomainRoute(
                    hostnames,
                    "ParameterSubdomain1",
                    "{parameter2}",
                    "{id}",
                    new { controller = "Home", action = "Action3" });

                routes.MapSubdomainRoute(
                    hostnames,
                    "ParameterSubdomain4",
                    "{area}",
                    "{controller}/{action}/{id}");

                routes.MapSubdomainRoute(
                    hostnames,
                    "ParameterSubdomain2",
                    "{controller}",
                    "{action}/{id}",
                    new { controller = "Home" });

                routes.MapSubdomainRoute(
                    hostnames,
                    "ParameterSubdomain3",
                    "{parameter1}",
                    "",
                    new { controller = "Home", action = "Action2" });

                routes.MapRoute(hostnames, 
                "NormalRoute", 
                "{controller}/{action}", 
                new { Action = "Index", Controller = "Home" });
            });
        }
    }
}
