using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Devi.ParkingService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new Microsoft.AspNetCore.Hosting.WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseUrls("http://*:5000")
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
