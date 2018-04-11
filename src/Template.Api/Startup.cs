using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;
using Template.Api.Utils;
using Template.DAL;
using Template.DAL.Customers;
using Template.DAL.EfContext;
using Template.DAL.Movies;
using Template.Infrastructure;

namespace Template.Api
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
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddSingleton(_configuration);
            services.Configure<ApiSettings>(_configuration.GetSection("Settings"));

            services.AddDbContext<ApplicationContext>(opts => opts.UseSqlServer(_configuration["ConnectionString:Default"]));

            services.TryAddScoped<IUnitOfWork, UnitOfWork>();
            services.TryAddTransient<MovieRepository>();
            services.TryAddTransient<CustomerRepository>();

            services
                .AddMvc();
                //.AddMvcOptions(c => { c.OutputFormatters.RemoveType<JsonOutputFormatter>(); });

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
            app.UseMiddleware<ExceptionHandler>();

            /*Enabling swagger file*/
            app.UseStaticFiles();
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
