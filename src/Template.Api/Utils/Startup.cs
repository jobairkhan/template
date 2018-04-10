﻿using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;
using Template.DAL;
using Template.DAL.Customers;
using Template.DAL.Movies;
using Template.Infrastructure;

namespace Template.Api.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(opts => opts.UseSqlServer(_configuration["ConnectionString:Default"]));;
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<MovieRepository>();
            services.AddTransient<CustomerRepository>();
            
            services.AddMvc();

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "Template API",
                    Version = "v1",
                    Description = "A sample rest api"
                });

                // Set the comments path for the Swagger JSON and UI.
                var basePath = Microsoft.Extensions.PlatformAbstractions.PlatformServices.Default.Application.ApplicationBasePath;
                var filePath = Path.Combine(basePath, $"{nameof(Template)}.{nameof(Api)}.xml");
                c.IncludeXmlComments(filePath);
            });
            Log.Debug("Services configuration completed.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();
            app.UseMiddleware<ExceptionHandler>();

            /*Enabling swagger file*/
            app.UseSwagger();
            /*Enabling Swagger ui, consider doing it on Development env only*/
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"{_configuration["virtual-directory"]}/swagger/v1/swagger.json", "Sample API V1");
                c.RoutePrefix = "docs";
            });


            app.UseMvc();
            Log.Debug("Application configuration completed.");
        }
    }
}
