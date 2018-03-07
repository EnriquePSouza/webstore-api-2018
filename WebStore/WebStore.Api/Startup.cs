using System;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using WebStore.Api.Security;
using WebStore.Domain.StoreContext.Handlers;
using WebStore.Domain.StoreContext.Repositories;
using WebStore.Infra;
using WebStore.Infra.Repositories;
using WebStore.Infra.StoreContext.Repositories;
using WebStore.Infra.Transactions;
using WebStore.Shared;

namespace WebStore.Api
{
    public class Startup
    {
        public static IConfiguration Configuration { get; set; }

        // Application Request Name
        private const string ISSUER = "e8j27j91";

        // Application Receive Name
        private const string AUDIENCE = "e2tttt729065";

        // Key to Use in Encryption Process
        private const string SECRET_KEY = "e8j27j91-9183-6b58-e525-e2tttt729065";

        // Use to Generate a Symmetric Key
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SECRET_KEY));

        public Startup(IHostingEnvironment env)
        {
            var configurationBuilder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddEnvironmentVariables();

            Configuration = configurationBuilder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // AppSettings Configuration
            services.AddCors();
            services.AddMvc(config =>
            {
                // Request Authentication to Allow Access to API 
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });

            // Assigning Token Configurations and Authorizations
            services.AddAuthorization(options =>
            {
                // System Policies Definition - Policy is an Authorization Contract
                options.AddPolicy("User", policy => policy.RequireClaim("WebStore", "User"));
                options.AddPolicy("Admin", policy => policy.RequireClaim("WebStore", "Admin"));
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = ISSUER,

                ValidateAudience = true,
                ValidAudience = AUDIENCE,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = true,
                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };
            
            // Authentication Bearer Options
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Audience = Configuration["MySettings:Auth0Settings:Audience"];
                options.Authority = Configuration["MySettings:Auth0Settings:Authority"];
                options.TokenValidationParameters = tokenValidationParameters;
            });

            services.Configure<TokenOptions>(options =>
            {
                options.Issuer = ISSUER;
                options.Audience = AUDIENCE;
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            // Adding Compression
            services.AddResponseCompression();

            // Resolving Dependency Injection
            services.AddScoped<DataAccessManager, DataAccessManager>();
            services.AddTransient<IUow, Uow>();

            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();

            services.AddTransient<CustomerHandler, CustomerHandler>();
            services.AddTransient<OrderHandler, OrderHandler>();

            // Add Swagger Documentation
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Info { Title = "Web Store", Version = "v1" });
            });

            // "appsettings.json" Constants Reading Declaration
            Settings.ConnectionString = $"{Configuration["connectionString"]}";
        }

        // Execute All Basic Settings in Aplication Startup
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseCors(x =>
            {
                x.AllowAnyHeader();
                x.AllowAnyMethod();
                x.AllowAnyOrigin();
            });

            app.UseAuthentication();
            app.UseResponseCompression();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web Store - V1");
            });
        }
    }
}