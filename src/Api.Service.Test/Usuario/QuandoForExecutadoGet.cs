using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;

namespace Api.Service.Test.Usuario
{
  public class QuandoForExecutadoGet : UsuarioTestes
  {
    private IUserService _service;
    private Mock<IUserService> _serviceMock;

    [Fact(DisplayName = "É possível executar o método get.")]
    public async Task E_Possivel_Executar_Get()
    {
      _serviceMock = new Mock<IUserService>();
      _serviceMock.Setup(m => m.Get(IdUsuario)).ReturnsAsync(userDto);

      _service = _serviceMock.Object;

      var result = await _service.Get(IdUsuario);
      Assert.NotNull(result);
      Assert.True(result.Id == IdUsuario);
      Assert.Equal(NomeUsuario, result.Name);
    }
  }
}
