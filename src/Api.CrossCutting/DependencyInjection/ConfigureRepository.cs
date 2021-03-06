using System;
using Api.Data.Context;
using Api.Data.Implementations;
using Api.Data.Repository;
using Api.Domain.Interfaces;
using Api.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection
{
  public class ConfigureRepository
  {
    public static void ConfigureDependenciesRepository(IServiceCollection serviceCollection)
    {
      serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
      serviceCollection.AddScoped<IUserRepository, UserImplementation>();
      serviceCollection.AddScoped<ICepRepository, CepImplementation>();
      serviceCollection.AddScoped<IMunicipioRepository, MunicipioImplementation>();
      serviceCollection.AddScoped<IUfRepository, UfImplementation>();

      if (Environment.GetEnvironmentVariable("DATABASE").ToLower() == "SQLSERVER".ToLower())
      {
        serviceCollection.AddDbContext<MyContext>(options => options.UseMySql(Environment.GetEnvironmentVariable("Db_CONNECTION")));
      }
      else
      {
        serviceCollection.AddDbContext<MyContext>(options => options.UseMySql(Environment.GetEnvironmentVariable("Db_CONNECTION")));
      }
    }
  }
}
