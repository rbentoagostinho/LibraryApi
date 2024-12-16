using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace LibraryApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Inicia o host da aplica��o
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // Configura a classe Startup para inicializar a aplica��o
                    webBuilder.UseStartup<Startup>();
                });
    }
}
