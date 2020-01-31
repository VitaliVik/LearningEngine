using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using LearningEngine.Persistence.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace LearningEngine.IntegrationTests
{
    public class CustomWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup: class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<LearnEngineContext>));
               
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<LearnEngineContext>(options =>
                {
                    options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=LearningEngineTestDb; Trusted_Connection=true;");
                });

                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<LearnEngineContext>();
                    db.Database.EnsureDeleted();
                    db.Database.EnsureCreated();
                    try
                    {
                        Utilities.InitializeDbForTests(db);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            });

        }
    }
}
