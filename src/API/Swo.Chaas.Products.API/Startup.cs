using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swo.Chaas.Products.Application;
using Swo.Chaas.Products.Domain.Entities.Features.Product.Repositories;
using Swo.Chaas.Products.Domain.Entities.Repositories;
using Swo.Chaas.Products.Persistence;
using Swo.Chaas.Products.Persistence.DbContext;

namespace Swo.Chaas.Products.API
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

            services.AddDbContext<ProductsDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ProductsConnectionString")));

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
                        Name = "SoftwareONE Company",
                        Email = "ihidalgo@intergrupo.com"
                    }
                });
            });
        }
    }
}
