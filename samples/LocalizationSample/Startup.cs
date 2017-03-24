using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace LocalizationSample
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();

            services.AddSession();

            services.AddMvc(options =>
            {
                options.Filters.Add(new MiddlewareFilterAttribute(typeof(LocalizationPipeline)));
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSession();

            app.Use(async (context, next) =>
            {
                context.Session.SetString("culture", "ar-YE");
                context.Session.SetString("ui-culture", "ar-YE");

                await next();
            });

            app.UseMvc();
        }
    }
}
