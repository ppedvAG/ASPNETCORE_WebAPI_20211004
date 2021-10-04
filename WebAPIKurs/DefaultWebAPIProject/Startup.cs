using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefaultWebAPIProject
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
            services.AddControllers(); //WebAPI wird hier verwendet

            services.AddSwaggerGen(c => //Dokumentation / Testing-Tool  über unsere WebAPI erhalten 
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DefaultWebAPIProject", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Welche Feature sollen nur Entwickler zugänglich gemacht werden?
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); //ExceptionPage für das Darstellen von detailierten Fehlern. 


                //Swagger -> Um WebAPI zu Testen und Dokumentation einzusehen. 
                app.UseSwagger(); 
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DefaultWebAPIProject v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); //MapControllers wird in kombination mit services.AddControllers();  verwendet!!!!
            });
        }
    }
}
