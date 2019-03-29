using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using web_layout_portal.Components;

namespace web_layout_portal
{
    public class Startup
    {
        private readonly ILogger _logger;
        private readonly ILoggerFactory _loggerFactory;

        public Startup(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
//            services.Configure<RazorViewEngineOptions>(opts =>
//                
//                opts.FileProviders.Add(
//                    new UrlProvider(_loggerFactory.CreateLogger(typeof(UrlProvider)))
//                ));
            services.AddTransient<RemoteViewResult>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }

//    public class UrlProvider : IFileProvider
//    {
//        private readonly ILogger _logger;
//        private HttpClient _client;
//
//        public UrlProvider(ILogger logger)
//        {
//            _logger = logger;
//            _client = new HttpClient()
//            {
//                BaseAddress = new Uri("http://localhost:5001/")
//            };
//        }
//
//        public IFileInfo GetFileInfo(string subpath)
//        {
//            _logger.LogInformation($"GetFileInfo: " + subpath);
//            var result = _client.SendAsync(new HttpRequestMessage(HttpMethod.Options, subpath)).Result;
//
//            return new RemoteFileInfo(result, _client);
//        }
//
//        public IDirectoryContents GetDirectoryContents(string subpath)
//        {
//            _logger.LogInformation($"GetDirectoryContents: " + subpath);
//            return new NotFoundDirectoryContents();
//        }
//
//        public IChangeToken Watch(string filter)
//        {
//            _logger.LogInformation($"Watch: " + filter);
//            return new CancellationChangeToken(CancellationToken.None);
//        }
//    }
//
//    public class RemoteFileInfo : IFileInfo
//    {
//        private readonly HttpResponseMessage _response;
//        private readonly HttpClient _client;
//
//        public RemoteFileInfo(HttpResponseMessage response, HttpClient client)
//        {
//            _response = response;
//            _client = client;
//
//            Name = response.RequestMessage.RequestUri.AbsolutePath;
//            if (response.IsSuccessStatusCode)
//            {
//                Exists = true;
//                if (response.Headers.TryGetValues("Content-Length", out var values) &&
//                    int.TryParse(values.FirstOrDefault(), out var length))
//                    Length = length;
//            }
//        }
//
//        public Stream CreateReadStream()
//        {
//            var data = _client.GetByteArrayAsync(_response.RequestMessage.RequestUri).Result;
//            var memoryStream = new MemoryStream(data);
//            return memoryStream;
//        }
//
//        public bool Exists { get; }
//        public long Length { get; }
//        public string PhysicalPath { get; }
//        public string Name { get; }
//        public DateTimeOffset LastModified { get; }
//        public bool IsDirectory { get; }
//    }
}
