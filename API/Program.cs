using System;
using MoveMoney.API.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Infraestructure.Persistence;

namespace MoveMoney.API
{
    public class Program
    {
        public static string EventType = "";
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<DataContext>();
                    context.Database.Migrate();
                    Seed.SeedCountry(context);
                    Seed.SeedUserRole(context);
                    Seed.SeedTypeIdentification(context);
                    Seed.SeedAgency(context);
                    Seed.SeedCustomers(context);
                    Seed.SeedUser(context);
                    Seed.SeedComission(context);
                    Seed.SeedComissionRanges(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error has occured during migration.");
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
