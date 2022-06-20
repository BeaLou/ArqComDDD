using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Api.Data.Context
{
  public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
  {
    public MyContext CreateDbContext(string[] args)
    {
      //Usado para a criação de migrations

      //MySQL
      // var connectionString = "Server=localhost;Port=3306;Database=dbarquiteturacomddd;Uid=root;Pwd=1234";
      // optionsbuilder.UseMySql(connectionString);

      //SQL Server
      var connectionString = "Server=BEATRIZ\\SQLEXPRESS;Database=dbarquiteturacomddd;MultipleActiveResultSets=true;User ID=sa;Password=1234";
      var optionsbuilder = new DbContextOptionsBuilder<MyContext>();

      optionsbuilder.UseSqlServer(connectionString);

      return new MyContext(optionsbuilder.Options);
    }
  }
}
