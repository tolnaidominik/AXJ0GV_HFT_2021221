using AXJ0GV_HFT_2021221.Data;
using AXJ0GV_HFT_2021221.Logic;
using AXJ0GV_HFT_2021221.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AXJ0GV_HFT_2021221.Endpoint
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve);

            services.AddTransient<IDogLogic, DogLogic>();
            services.AddTransient<IOwnerLogic, OwnerLogic>();
            services.AddTransient<IInjectionLogic, InjectionLogic>();
            
            services.AddTransient<IDogRepository, DogRepository>();
            services.AddTransient<IOwnerRepository, OwnerRepository>();
            services.AddTransient<IInjectionRepository, InjectionRepository>();


            services.AddTransient<DogDbContext, DogDbContext>();
            services.AddSignalR();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseCors(x => x
            .AllowCredentials()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithOrigins("http://localhost:31666"));
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
            endpoints.MapControllers();
            endpoints.MapHub<SignalRHub>("/hub");
            });
        }
    }
}
