﻿using System;
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
using InzynierkaWebService.Helpers;

namespace InzynierkaWebService
{
    public partial class Startup
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
            services.AddDbContext<CostSharingContext>(options => options.UseSqlServer(Configuration.GetSection("SecretStrings")["DefaultConnection"]));

            // Add framework services.
            services.AddMvc();
            services.AddCors();
            services.AddSingleton<ICostRepository, CostRepository>();
            services.AddSingleton<IGroupRepository, GroupRepository>();
            services.AddSingleton<IMemberRepository, MemberRepository>();
            services.AddSingleton<IInstanceRepository, InstanceRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<ICostTypeRepository, CostTypeRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            ConfigureAuth(app);

            app.UseCors(builder =>
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod()
            );

            app.UseMvc();
        }
    }
}
