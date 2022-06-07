using System.ComponentModel.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.CrossCutting.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace application
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
      ConfigureService.ConfigureDependenciesService(services);
      ConfigureRepository.ConfigureDependenciesRepository(services);

      services.AddControllers();
      services.AddSwaggerGen(x =>
      {
        x.SwaggerDoc("v1",
          new OpenApiInfo
          {
            Version = "V1",
            Title = "Curso API - Projeto bea",
            Description = "Arquitetura DDD",
            TermsOfService = new Uri("https://github.com/bealou"),
            Contact = new OpenApiContact
            {
              Name = "Beatriz Ramos Lourenço",
              Email = "beatrizlourenco1912@gmail.com"
            }
            //aqui eu posso adicionar uma licença
          });
      });
    }
    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseSwagger();
      app.UseSwaggerUI(x =>
      {
        x.SwaggerEndpoint("/swagger/v1/swagger.json", "Curso API - ASP NET CORE com DDD");
        x.RoutePrefix = string.Empty;
      });
      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
