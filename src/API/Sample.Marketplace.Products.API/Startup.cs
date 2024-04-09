using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sample.Marketplace.Products.Application;
using Sample.Marketplace.Products.Domain.Entities.Features.Product.Repositories;
using Sample.Marketplace.Products.Domain.Entities.Repositories;
using Sample.Marketplace.Products.Persistence;
using Sample.Marketplace.Products.Persistence.DbContext;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Sample.Marketplace.Products.Application.Profiles;

namespace Sample.Marketplace.Products.API
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
            AddSwagger(services);

            services.AddApplicationServices();
            var configurationBuilder = new ConfigurationBuilder();
            IConfiguration configuration = configurationBuilder.Build();

            var variables = Environment.GetEnvironmentVariables();
            var server = Environment.GetEnvironmentVariable("MYSQL_SERVER");
            var dataBaseConnectionString = $"Server={Environment.GetEnvironmentVariable("MYSQL_SERVER")}; User ID={Environment.GetEnvironmentVariable("MYSQL_USER")}; Password={Environment.GetEnvironmentVariable("MYSQL_PASSWORD")}; Database=products;";
            
            services.AddDbContext<ProductsDbContext>(options => options.UseMySql(dataBaseConnectionString, ServerVersion.AutoDetect(dataBaseConnectionString)));

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ISkuRepository, SkuRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISkuService, SkuService>();

            services.AddControllersWithViews().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = "https://dev-3ipf5qm2.auth0.com";
                options.Audience = "cypress";
            });

            services.AddAuthorization(o =>
            {
                o.AddPolicy("read:products", p => p.
                    RequireAuthenticatedUser().
                    RequireClaim("permissions", "read:products"));

                o.AddPolicy("create:products", p => p.
                    RequireAuthenticatedUser().
                    RequireClaim("permissions", "create:products"));

                o.AddPolicy("delete:products", p => p.
                    RequireAuthenticatedUser().
                    RequireClaim("permissions", "delete:products"));
            });
            
            services.AddAutoMapper(typeof(MappingProfile),typeof(MappingProductProfile));



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

            app.UseSwagger();
            app.UseSwaggerUI( c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Products Management API");
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var groupName = "v1";

                options.SwaggerDoc(groupName, new OpenApiInfo
                {
                    Title = $"Products {groupName}",
                    Version = groupName,
                    Description = "Products Management API",
                    Contact = new OpenApiContact
                    {
                        Name = "Labs Company",
                        Email = "ivanhidalgo22@gmail.com"
                    }
                });
            });
        }
    }
}
