using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Steeltoe.Extensions.Logging;

namespace gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging((cfg,log) =>
                {
                    log.AddDynamicConsole(cfg.Configuration.GetSection("Logging"));
                })
                .ConfigureAppConfiguration(cfg => cfg.AddJsonFile("ocelot.json"))
                .UseStartup<Startup>()
                .Build()
                .Run();
        }

     
    }
}
