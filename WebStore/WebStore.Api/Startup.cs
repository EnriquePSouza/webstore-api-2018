using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebStore.Domain.StoreContext.Handlers;
using Swashbuckle.AspNetCore.Swagger;
using WebStore.Domain.StoreContext.Repositories;
using WebStore.Infra;
using WebStore.Infra.StoreContext.Repositories;
using WebStore.Shared;

namespace WebStore.Api
{
    public class Startup
    {
        public static IConfiguration Configuration { get; set; }
        public void ConfigureServices(IServiceCollection services)
        {
            // AppSettings Configuration
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            services.AddMvc();

            // Adding Compression
            services.AddResponseCompression();

            // Resolving Dependency Injection
            services.AddScoped<DataAccessManager, DataAccessManager>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<CustomerHandler, CustomerHandler>();
            //Verify EmailService Before Resolving

            // Add Swagger Documentation
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Info { Title = "Web Store", Version = "v1" });
            });

            // "appsettings.json" constants reading declaration
            Settings.ConnectionString = $"{Configuration["connectionString"]}";
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseMvc();
            app.UseResponseCompression();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web Store - V1");
            });
        }
    }
}