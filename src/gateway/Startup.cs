using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Eureka;
using Pivotal.Discovery.Client;
//using Steeltoe.Management.CloudFoundry;
//using Steeltoe.Management.Endpoint;
//using Steeltoe.Management.Endpoint.Env;
//using Steeltoe.Management.Endpoint.Refresh;
//using Steeltoe.Management.Hypermedia;

namespace gateway
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
//            services.AddCloudFoundryActuators(Configuration, MediaTypeVersion.V2, ActuatorContext.ActuatorAndCloudFoundry);
//            services.AddRefreshActuator(Configuration);
//            services.AddEnvActuator(Configuration);
            services.AddOcelot(Configuration)
                .AddEureka();
            return services.BuildServiceProvider(false);
        }

        public void Configure(IApplicationBuilder app)
        {
//            app.UseCloudFoundryActuators(MediaTypeVersion.V2, ActuatorContext.ActuatorAndCloudFoundry);
//            app.UseRefreshActuator();
//            app.UseEnvActuator();
            app.UseOcelot().Wait();
        }
    }
}
