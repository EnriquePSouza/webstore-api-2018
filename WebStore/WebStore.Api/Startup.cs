using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using WebStore.Domain.StoreContext.Handlers;
using WebStore.Domain.StoreContext.Repositories;
using WebStore.Infra;
using WebStore.Infra.StoreContext.Repositories;

namespace WebStore.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            // Resolving Dependency Injection
            services.AddScoped<DataAccessManager, DataAccessManager>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<CustomerHandler, CustomerHandler>();
            //Verify EmailService Before Resolving
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseMvc();
        }
    }
}