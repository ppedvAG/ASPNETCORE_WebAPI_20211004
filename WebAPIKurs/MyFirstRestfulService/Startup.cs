using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MyFirstRestfulService.Data;
using MyFirstRestfulService.Formatter;
using MyFirstRestfulService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiContrib.Core.Formatter.Csv;

namespace MyFirstRestfulService
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

            services.AddControllers(options =>
            {
                options.InputFormatters.Insert(0, new VCardInputFormatter());
                options.OutputFormatters.Insert(0, new VCardOutputFormatter());


                options.InputFormatters.Insert(1, GetJsonPatchInputFormatter());


                //options.InputFormatters.Insert(2, new VCardInputFormatter());
                //options.OutputFormatters.Insert(2, new VCardOutputFormatter());
            })
                .AddNewtonsoftJson()
                .AddXmlSerializerFormatters()
                .AddCsvSerializerFormatters();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyFirstRestfulService", Version = "v1" });
            });

           services.AddDbContext<MovieDbContext>(options =>
           {
               options.UseInMemoryDatabase("MovieDB");
           });


            services.AddHttpClient<IVideoStream, VideoStream>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyFirstRestfulService v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter()
        {
            var builder = new ServiceCollection()
                .AddLogging()
                .AddMvc()
                .AddNewtonsoftJson()
                .Services.BuildServiceProvider();


            return builder
                .GetRequiredService<IOptions<MvcOptions>>()
                .Value
                .InputFormatters
                .OfType<NewtonsoftJsonPatchInputFormatter>()
                .First();
        }

    }
}
