using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using InzynierkaWebService.Models;

namespace InzynierkaWebService
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //optionsBuilder.UseSqlServer(@"Data Source=PAWLOWYPC;Initial Catalog=inzynierka;Integrated Security=True;Trusted_Connection=True;");

            //var connection = @"Server=(localdb)\mssqllocaldb;Database=Blogging;Trusted_Connection=True;";
            //var connection = @"Data Source=PAWLOWYPC;Initial Catalog=inzynierka;Integrated Security=True;Trusted_Connection=True;";
            var connection = @"Server=tcp:cost-sharing-server.database.windows.net,1433;Initial Catalog=costSharingDB;Persist Security Info=False;User ID=introozAdmin;Password=azureadminPassword1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            services.AddDbContext<InzynierkaContext>(options => options.UseSqlServer(connection));

            // Add framework services.
            services.AddMvc();

            services.AddSingleton<ICostRepository, CostRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
        }
    }
}
