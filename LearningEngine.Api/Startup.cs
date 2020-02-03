using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using LearningEngine.Api.Authorization;
using MediatR;
using LearningEngine.Persistence.Models;
using LearningEngine.Api.Services;
using Microsoft.EntityFrameworkCore;

namespace LearningEngine.Api
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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = AuthOptions.ISSUER,
                        ValidateAudience = true,
                        ValidAudience = AuthOptions.AUDIENCE,
                        ValidateLifetime = true,
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true
                    };
                });
            services.AddTransient<IEnviromentService, EnviromentService>();
            services.AddTransient<IConfigurationService, ConfigurationService>(provider =>
            new ConfigurationService(provider.GetService<IEnviromentService>()));

            //services.AddScoped(provider =>
            //{
            //    var configureService = provider.GetService<IConfigurationService>();
            //    var connectionString = configureService.GetConfiguration().GetConnectionString(nameof(LearnEngineContext));
            //    var optionsBuilder = new DbContextOptionsBuilder<LearnEngineContext>();
            //    optionsBuilder.UseSqlServer(connectionString, builder => builder.MigrationsAssembly("LearningEngine.Persistence"));
            //    return new LearnEngineContext(optionsBuilder.Options);
            //});
            services.AddDbContext<LearnEngineContext>((provider, opt)  => 
            {
                var configureService = provider.GetService<IConfigurationService>();
                var connectionString = configureService.GetConfiguration().GetConnectionString(nameof(LearnEngineContext));
                opt.UseSqlServer(connectionString, builder => builder.MigrationsAssembly("LearningEngine.Persistence")); 
            });
        
            services.AddControllers();
            services.AddMediatR(typeof(LearningEngine.Persistence.Handlers.GetIdentityHandler).Assembly);
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}