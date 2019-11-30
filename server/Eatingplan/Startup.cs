using System;
using System.IO;
using Eatingplan.Logging.Extensions;
using Eatingplan.Utils;
using Bazinga.AspNetCore.Authentication.Basic;
using Castle.Windsor;
using Castle.Windsor.MsDependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Eatingplan
{
    internal class Startup
    {
        private const string AllowLocalhostAndLive = "MyCorsPolicy";
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<RecipeContext>(opt => opt.UseInMemoryDatabase("Recipes"));
            services.AddCors(options =>
            {
                options.AddPolicy(AllowLocalhostAndLive,
                    builder =>
                    {
                        builder.WithOrigins("https://gardening.get-it-working.com",
                            "http://localhost:4200")
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                    }
                    );
            });
            //todo what is the difference between AddMvc and AddMvcCore
            services.AddMvc()
                .AddMvcOptions(opt => opt.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddAuthentication(BasicAuthenticationDefaults.AuthenticationScheme)
                .AddBasicAuthentication<BasicAuthenticationVerifier>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Eating plan API",
                    Version = "v1",
                    Description = "API for interacting with the eating plan"
                });
                var filePath = Path.Combine(System.AppContext.BaseDirectory, "Eatingplan.xml");
                c.IncludeXmlComments(filePath);
            });

            var windsorContainer = new WindsorContainer();
            windsorContainer.Install(new Installer());
            return WindsorRegistrationHelper.CreateServiceProvider(windsorContainer, services);
        }

        public void Configure(IApplicationBuilder appBuilder, Microsoft.Extensions.Logging.ILoggerFactory loggerFactory)
        {
            loggerFactory.AddLog4Net(Constants.Log4NetConfigFile);
            appBuilder.UseCors(AllowLocalhostAndLive);
            appBuilder.UseAuthentication();
            appBuilder.UseMvc();
            appBuilder.UseSwagger(o => o.RouteTemplate = "/api-docs/{documentName}/swagger.json");
            appBuilder.UseSwaggerUI(o =>
            {
                o.RoutePrefix = "api-docs";
                o.SwaggerEndpoint("/api-docs/v1/swagger.json", "My API v1");
            });
        }
    }
}
