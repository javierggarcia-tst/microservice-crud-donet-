using System;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CRUDBasico.Infrastructure.Filters;
using CRUDBasico.Infrastructure.BD;
using CRUDBasico.Configuration;
using CRUDBasico.Infrastructure.Specification;
using CRUDBasico.Infrastructure.BD.Repository;
using AutoMapper;
using CRUDBasico.Mapping;
using CRUDBasico.Model;
using CRUDBasico.Servicio.Kraken;

namespace CRUDBasico
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private const string ConnectionKrakenString = "ConnectionKrakenString";

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string conexion = _configuration[ConnectionKrakenString];

            services.AddControllers();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerDocumentation();

            /*AutoMapper*/
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AtributoProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            /* Servicio Kraken */
            services.AddSingleton<IKraken>(new Kraken(conexion));

            services
               .AddCustomMVC(_configuration)
               .AddCustomDbContext(_configuration)
               .AddCustomOptions(_configuration);

            services.AddTransient<IAtributosSpecification, AtributoSpecification>();
            services.AddTransient<IAtributosRepository, AtributoRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            app.UseSwaggerDocumentation();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }

    public static class CustomExtensionMethods
    {
        private const string OrdersConnectionString = "OrdersConnectionString";

        public static IServiceCollection AddCustomMVC(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
            })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddControllersAsServices();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            return services;
        }

        public static IServiceCollection AddCustomHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            //var accountName = configuration.GetValue<string>("AzureStorageAccountName");
            //var accountKey = configuration.GetValue<string>("AzureStorageAccountKey");

            //var hcBuilder = services.AddHealthChecks();

            //hcBuilder
            //    .AddCheck("self", () => HealthCheckResult.Healthy())
            //    .AddSqlServer(
            //        configuration["ConnectionString"],
            //        name: "CatalogDB-check",
            //        tags: new string[] { "catalogdb" });

            //if (!string.IsNullOrEmpty(accountName) && !string.IsNullOrEmpty(accountKey))
            //{
            //    hcBuilder
            //        .AddAzureBlobStorage(
            //            $"DefaultEndpointsProtocol=https;AccountName={accountName};AccountKey={accountKey};EndpointSuffix=core.windows.net",
            //            name: "catalog-storage-check",
            //            tags: new string[] { "catalogstorage" });
            //}

            //if (configuration.GetValue<bool>("AzureServiceBusEnabled"))
            //{
            //    hcBuilder
            //        .AddAzureServiceBusTopic(
            //            configuration["EventBusConnection"],
            //            topicName: "eshop_event_bus",
            //            name: "catalog-servicebus-check",
            //            tags: new string[] { "servicebus" });
            //}
            //else
            //{
            //    hcBuilder
            //        .AddRabbitMQ(
            //            $"amqp://{configuration["EventBusConnection"]}",
            //            name: "catalog-rabbitmqbus-check",
            //            tags: new string[] { "rabbitmqbus" });
            //}

            return services;
        }

        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BDContext>(options =>
            {
                options.UseSqlServer(configuration[OrdersConnectionString],
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                        
                        //Configuring Connection Resiliency:
                        sqlOptions.
                            EnableRetryOnFailure(maxRetryCount: 5,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null);
                    });
            });

            return services;
        }

        public static IServiceCollection AddCustomOptions(this IServiceCollection services, IConfiguration configuration)
        {
            //services.Configure<CatalogSettings>(configuration);
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Instance = context.HttpContext.Request.Path,
                        Status = StatusCodes.Status400BadRequest,
                        Detail = "Please refer to the errors property for additional details."
                    };

                    return new BadRequestObjectResult(problemDetails)
                    {
                        ContentTypes = { "application/problem+json", "application/problem+xml" }
                    };
                };
            });

            return services;
        }
    
    }
}
