using Cds.BusinessCustomer.Domain.CustomerAggregate.Abstractions;
using Cds.BusinessCustomer.Infrastructure.CustomerRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Cds.BusinessCustomer.Api.Bootstrap
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

            // Swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Customer API",
                        Description = "Informations about my Customer API - Swagger",
                        Contact = new OpenApiContact
                        {
                            Name = "Imane Abdennour",
                            Email = "imane.abdennour0@gmail.com",
                        },
                        Version = "v1",

                    });
                // Set the comments path for the Swagger JSON and UI.    
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            // HealthChecks :
            services.AddHealthChecks();
            //// DB
            //services.AddDbContext<CustomerContext>(opt => opt.UseSqlServer
            //  (Configuration.GetConnectionString("CustomerConnection"))     // string is the one specified in appsettings.json
            //);

            // Automapper :
           // services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // DI : 
            services.AddScoped<ICartegieRepository, CartegieRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Swagger :
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Customer API");
            });

            app.UseHttpsRedirection();

            // HealthChecks :
            app.UseHealthChecks("/healthCheck");
        }
    }
}
