using System.Threading.Tasks;
using Api.Domain.Dtos.User;
using Xunit;

namespace Api.Integration.Test.Usuario
{
  public class QuandoRequisitarUsuario : BaseIntegration
  {
    private string _name { get; set; }
    private string _email { get; set; }

    [Fact]
    public async Task E_Possivel_Realizar_Crud_Usuario()
    {
      await AdicionarToken();
      _name = Faker.Name.First();
      _email = Faker.Internet.Email();

      var userDto = new UserDTOCreate()
      {
        Name = _name,
        Email = _email
      };
    }
  }
}
