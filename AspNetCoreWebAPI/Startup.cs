using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.IO;
using AspNetCoreWebAPI.Services;
using AspNetCoreWebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreWebAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true);

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            var conn = Configuration["connectionStrings:sqlConnection"];
            services.AddDbContext<SqlDbContext>(options =>
                options.UseSqlServer(conn));

            services.AddScoped(typeof(IBookstoreRepository), typeof(BookstoreSqlRepository));
            services.AddScoped(typeof(IGenericEFRepository), typeof(GenericEFRepository));

            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<Entities.Book, Models.BookDTO>();
                config.CreateMap<Models.BookDTO, Entities.Book>();
                config.CreateMap<Entities.Publisher, Models.PublisherDTO>();
                config.CreateMap<Models.PublisherDTO, Entities.Publisher>();
                config.CreateMap<Models.PublisherUpdateDTO, Entities.Publisher>();
                config.CreateMap<Entities.Publisher, Models.PublisherUpdateDTO>();
                config.CreateMap<Models.BookUpdateDTO, Entities.Book>();
                config.CreateMap<Entities.Book, Models.BookUpdateDTO>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseStatusCodePages();

            app.UseMvc();
            //app.Run(async (context) =>
            //{
            //    var message = Configuration["Message"];
            //    await context.Response.WriteAsync(message);
            //});
        }
    }
}
