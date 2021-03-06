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
using Api.Domain.Security;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Api.CrossCutting.Mappings;
using AutoMapper;
using Api.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace application
{
  public class Startup
  {
    public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
    {
      Configuration = configuration;
      _webHostEnvironment = webHostEnvironment;
    }

    public IConfiguration Configuration { get; }
    public IWebHostEnvironment _webHostEnvironment { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      if (_webHostEnvironment.IsEnvironment("Testing"))
      {
        Environment.SetEnvironmentVariable("DB_CONNECTION", "Server=localhost;Port=3306;Database=dbarquiteturacomddd1;Uid=root;Pwd=1234");
        Environment.SetEnvironmentVariable("DATABASE", "MYSQL");
        Environment.SetEnvironmentVariable("MIGRATION", "APLICAR");
        Environment.SetEnvironmentVariable("Audience", "ExemploAudience");
        Environment.SetEnvironmentVariable("ExemploIssue", "ExemploIssue");
        Environment.SetEnvironmentVariable("28800", "28800");
      }
      services.AddControllers();

      ConfigureService.ConfigureDependenciesService(services);
      ConfigureRepository.ConfigureDependenciesRepository(services);

      var config = new AutoMapper.MapperConfiguration(x =>
      {
        x.AddProfile(new DtoToModelProfile());
        x.AddProfile(new EntityToDtoProfile());
        x.AddProfile(new ModelToEntityProfile());
      });

      IMapper mapper = config.CreateMapper();
      services.AddSingleton(mapper);

      var signingConfigurations = new SigningConfigurations();
      services.AddSingleton(signingConfigurations);

      services.AddAuthentication(authOptions =>
                  {
                    authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                  }).AddJwtBearer(bearerOptions =>
                  {
                    var paramsValidation = bearerOptions.TokenValidationParameters;
                    paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                    paramsValidation.ValidAudience = Environment.GetEnvironmentVariable("Audience");
                    paramsValidation.ValidIssuer = Environment.GetEnvironmentVariable("Issuer");

                    // Valida a assinatura de um token recebido
                    paramsValidation.ValidateIssuerSigningKey = true;

                    // Verifica se um token recebido ainda ?? v??lido
                    paramsValidation.ValidateLifetime = true;

                    // Tempo de toler??ncia para a expira????o de um token (utilizado
                    // caso haja problemas de sincronismo de hor??rio entre diferentes
                    // computadores envolvidos no processo de comunica????o)
                    paramsValidation.ClockSkew = TimeSpan.Zero;
                  });

      // Ativa o uso do token como forma de autorizar o acesso
      // a recursos deste projeto
      services.AddAuthorization(auth =>
      {
        auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                  .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme??????)
                  .RequireAuthenticatedUser().Build());
      });
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
              Name = "Beatriz Ramos Louren??o",
              Email = "beatrizlourenco1912@gmail.com"
            }
            //aqui eu posso adicionar uma licen??a
          });
        x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
          Description = "entre com o token jwt",
          Name = "Auth",
          In = ParameterLocation.Header,
          Type = SecuritySchemeType.ApiKey
        });

        x.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        }, new List<string>()
                    }
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

      if (Environment.GetEnvironmentVariable("MIGRATION").ToLower() == "APLICAR".ToLower())
      {
        using (var service = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
          using (var context = service.ServiceProvider.GetService<MyContext>())
          {
            context.Database.Migrate();
          }
        }
      }
    }
  }
}
