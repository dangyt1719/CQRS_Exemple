using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
//using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using NLog.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebIntegrations.Connected_Services.OCOWebIntegrations.Soap1c;
using Infrastructure.Implementation.Repositories;
using Infrastructure.Interfaces.RepositoryInterfaces;
using ZupIntWebApi.Settings;
using NeocaseProviderLibrary;

namespace ZupIntWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        private readonly string myPolicy = "CorsAllowedOrigins";

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var credentials = Configuration.GetSection("ZupServiceCredentials").Get<Credentials>();

            services.AddCors(options =>
            {
                options.AddPolicy(name: myPolicy,
                                  builder =>
                                  {
                                      builder//
                                      .WithOrigins("https://localhost:44377", "http://localhost:51262")
                                     // .AllowAnyOrigin()
                                      .AllowAnyMethod().AllowAnyHeader() //;//
                                      .AllowCredentials();
                                  });
            });

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddConsole();
                loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                loggingBuilder.AddNLog(Configuration);
            });

            services.AddControllers();
            services.AddNeocaseProviders();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ZupIntWebApi", Version = "v1" });
            });
            //services.AddAuthentication(NegotiateDefaults.AuthenticationScheme);           
            services.AddScoped(ifact => new InthOCOPortTypeClient(credentials.Login, credentials.Password));
            services.AddScoped<IPermissionRepositiry, PermissionRepositiry>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ZupIntWebApi v1"));
            }

            app.UseHttpsRedirection();
            

            app.UseRouting();

            app.UseCors(myPolicy);

            app.UseAuthorization();
            //app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
