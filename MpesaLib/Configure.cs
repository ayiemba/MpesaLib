using Microsoft.Extensions.DependencyInjection;
using MpesaLib.Interfaces;
using MpesaLib.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MpesaLib
{
    public class Configure
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add application services.
            services.AddTransient<IHttpClientService, HttpClientService>();
            
        }
    }
}
