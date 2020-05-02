using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace CarRent
{
    public class Program
    {
        public static bool IsDevelopment = false;

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Seed().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
