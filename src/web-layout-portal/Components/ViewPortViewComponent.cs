using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace web_layout_portal.Components
{
    public class ViewPortViewComponent : ViewComponent
    {

        public async Task<IViewComponentResult> InvokeAsync(string name)
        {
            return new RemoteViewResult(name);
 
        }
    }

    public class RemoteViewResult : IViewComponentResult
    {
        private readonly string _name;
        HttpClient _client = new HttpClient()
        {
            BaseAddress = new Uri("http://localhost:5001/")
        };

        public RemoteViewResult(string name)
        {
            _name = name;
        }

        public void Execute(ViewComponentContext context)
        {
            ExecuteAsync(context).Wait();
        }

        public async Task ExecuteAsync(ViewComponentContext context)
        {
            var data = await _client.GetStringAsync(_name);
            await context.Writer.WriteAsync(data);
        }
    }
}
