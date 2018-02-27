using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;
using SFP.Persistencia.Model;

namespace SFP.SIT.SESAI
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }
        private IMemoryCache Memorycache { set; get; }
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IMemoryCache cache)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            BaseDbMdl baseDbOrigen = new BaseDbMdl();
            baseDbOrigen.connectionString = Configuration.GetConnectionString("ORIGEN");
            baseDbOrigen.objDbConnection = Configuration.GetSection("DataBaseObj")["objCxn"].ToString();
            baseDbOrigen.objDbTransaccion = Configuration.GetSection("DataBaseObj")["objTran"].ToString();
            baseDbOrigen.objDbDataAdapter = Configuration.GetSection("DataBaseObj")["objDataAdapter"].ToString();

            BaseDbMdl baseDbDestino = new BaseDbMdl();
            baseDbDestino.connectionString = Configuration.GetConnectionString("DESTINO");
            baseDbDestino.objDbConnection = Configuration.GetSection("DataBaseObj")["objCxn"].ToString();
            baseDbDestino.objDbTransaccion = Configuration.GetSection("DataBaseObj")["objTran"].ToString();
            baseDbDestino.objDbDataAdapter = Configuration.GetSection("DataBaseObj")["objDataAdapter"].ToString();

            cache.Set("BD_ORIGEN", baseDbOrigen);
            cache.Set("BD_DESTINO", baseDbDestino);

            Memorycache = cache;
        }
    }
}
