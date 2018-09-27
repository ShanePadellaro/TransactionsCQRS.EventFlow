using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using EventFlow;
using EventFlow.AspNetCore.Extensions;
using EventFlow.Commands;
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
using TransactionsCQRS.API.Domain.Account.Events;
using TransactionsCQRS.API.Domain.Account.ReadModels;
using TransactionsCQRS.API.Infrastructure;

namespace TransactionsCQRS.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            
            services.AddSingleton(config);
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddEventFlow(o =>
                o.ConfigureMongoDb("mongodb://localhost", "test")
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
                    .AddAspNetCoreMetadataProviders());
            
            services.AddHostedService<TransactionProcessor>();

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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}