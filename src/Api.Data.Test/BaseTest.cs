using Xunit;

namespace Api.Data.Test;

public abstract class UnitTest1
{
  public BaseTest()
  {

  }

  public class DbTest : IDisposable
  {
    private string dataBaseName = $"dbApiTest_{Guid.NewGuid().ToString().Replace("-", string.Empty)}";
    public ServiceProvider ServiceProvider { get; private set; }

    public DbTeste()
    {
      var serviceCollection = new ServiceCollection();
      serviceCollection.AddDbContext<MyContext>(o =>
          o.UseMySql($"Persist Security Info=True;Server=127.0.0.1;Database={dataBaseName};User=root;Password=1234;"),
          ServiceLifetime.Transient
      );

      ServiceProvider = serviceCollection.BuildServiceProvider();
      using (var context = ServiceProvider.GetService<Mycontext>())
      {
        context.Database.EnsureCreated();
      }
    }
    public void Dispose()
    {
      using (var context = ServiceProvider.GetService<Mycontext>())
      {
        context.Database.EnsureDeleted();
      }
    }
  }
}
