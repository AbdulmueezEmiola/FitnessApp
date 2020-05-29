using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FitnessApp
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
            services.AddControllers();
            services.AddHttpClient("WRITER", c =>
            {
                c.BaseAddress = new Uri("http://fitness.write.data.com:56789");
                c.DefaultRequestHeaders.Add("Accept", "application/json");
            });
            services.AddHttpClient("READER", c =>
            {
                c.BaseAddress = new Uri("http://fitness.read.data.com:56789");
                c.DefaultRequestHeaders.Add("ACCEPT", "application/json");
            });
            services.AddSingleton<IDataStoreClient, FitnessDataStoreClient>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
