using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApi.DBOperations;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // in memorye özel dependency injectiondan geliyor
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope()){
                var services = scope.ServiceProvider;
                DataGenerator.Initialize(services); 
            }
            // uygulama çalışırken veriler hep database e yazılacak
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
