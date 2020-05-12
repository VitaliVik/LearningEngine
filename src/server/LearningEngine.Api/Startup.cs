using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using LearningEngine.Api.Authorization;
using MediatR;
using LearningEngine.Persistence.Models;
using LearningEngine.Api.Services;
using Microsoft.EntityFrameworkCore;
using LearningEngine.Domain.Interfaces;
using LearningEngine.Persistence.Transaction;
using LearningEngine.Persistence.Utils;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;
using LearningEngine.Application.PipelineValidators;
using MediatR.Pipeline;

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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
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
            services.AddSingleton<IPasswordHasher>(sp => new PasswordHasher());
            services.AddTransient<IEnviromentService, EnviromentService>();
            services.AddScoped(typeof(IRequestPreProcessor<>),typeof(PipelinePermissionValidator<>));
            services.AddScoped<IJwtTokenCryptographer, JwtTokenCoder>();
            services.AddTransient<JwtSecurityTokenHandler>();
            services.AddTransient<IConfigurationService, ConfigurationService>(provider =>
            new ConfigurationService(provider.GetService<IEnviromentService>()));
            services.AddCors(options => options.AddPolicy("defaultPolicy",
                builder => builder.WithOrigins("http://localhost:3000").WithHeaders("Authorization")
                ));

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
            services.AddScoped<ITransactionUnitOfWork>(sp => 
            new TransactionUnitOfWork(sp.GetRequiredService<LearnEngineContext>()));
            services.AddControllers();
            services.AddMediatR(typeof(LearningEngine.Persistence.Handlers.GetIdentityHandler).Assembly, 
                typeof(LearningEngine.Application.UseCase.Handler.CreateUserThemeHandler).Assembly);
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseCors("defaultPolicy");
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "Learning Engine Api");
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
