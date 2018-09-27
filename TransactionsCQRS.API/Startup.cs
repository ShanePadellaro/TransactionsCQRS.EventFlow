﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using EventFlow;
using EventFlow.AspNetCore.Extensions;
using EventFlow.Commands;
using EventFlow.Configuration;
using EventFlow.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using EventFlow.Extensions;
using EventFlow.MongoDB.Extensions;
using EventFlow.Queries;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver.Core.Operations;
using TransactionsCQRS.API.Domain.Account.Events;
using TransactionsCQRS.API.Domain.Account.ReadModels;
using TransactionsCQRS.API.Infrastructure;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace TransactionsCQRS.API
{
    public class Startup
    {
        public static IServiceProvider ServiceProvider { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            
            services.AddSingleton(config);
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddHostedService<TransactionProcessor>();


            var provider = EventFlowOptions.New
                .UseServiceCollection(services)
                .ConfigureMongoDb("mongodb://localhost", "test")
                .UseMongoDbEventStore()
                .UseMongoDbSnapshotStore()
                .AddSnapshots(Assembly.GetExecutingAssembly())
                .AddCommands(Assembly.GetExecutingAssembly(), x => x.IsSubclassOf(typeof(Command<,>)))
                .AddCommandHandlers(Assembly.GetExecutingAssembly())
                .AddEvents(Assembly.GetExecutingAssembly())
                .UseMongoDbReadModel<AccountReadModel>()
                .RegisterServices(x => x.RegisterType(typeof(TransactionReadModelLocator)))
                .UseMongoDbReadModel<TransactionReadModel, TransactionReadModelLocator>()
                .AddQueryHandlers(Assembly.GetExecutingAssembly())
                .CreateServiceProvider();
           
            ServiceProvider = provider;
            return provider;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}