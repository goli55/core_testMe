using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CacheManager.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace ApiGetway
{
    public class Startup
    {
    public Startup(IHostingEnvironment env)  
    {  
        var builder = new Microsoft.Extensions.Configuration.ConfigurationBuilder();  
        builder.SetBasePath(env.ContentRootPath)      
               //add configuration.json  
               .AddJsonFile("configuration.json", optional: false, reloadOnChange: true)  
               .AddEnvironmentVariables();  
  
        Configuration = builder.Build();  
    }  

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Action<ConfigurationBuilderCachePart> settings = (x) =>  
        {  
            x.WithMicrosoftLogging(log =>  
            {  
                log.AddConsole(LogLevel.Debug);  
  
            }).WithDictionaryHandle();  
        };  
        services.AddOcelot(Configuration);  
                 
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

           await app.UseOcelot();  
        }
    }
}
