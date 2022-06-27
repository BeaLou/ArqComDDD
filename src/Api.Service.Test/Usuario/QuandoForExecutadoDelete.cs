using System;
using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;

namespace Api.Service.Test.Usuario
{
  public class QuandoForExecutadoDelete : UsuarioTestes
  {
    private IUserService _service;
    private Mock<IUserService> _serviceMock;

    [Fact(DisplayName = "É possível executar o método delete.")]
    public async Task E_Possivel_Executar_Delete()
    {
      //deletado
      _serviceMock = new Mock<IUserService>();
      _serviceMock.Setup(m => m.Put(userDtoUpdate)).ReturnsAsync(userDtoUpdateResult);
      _service = _serviceMock.Object;

      var deletado = await _service.Delete(IdUsuario);
      Assert.True(deletado);

      //não deletado
      _serviceMock = new Mock<IUserService>();
      _serviceMock.Setup(m => m.Put(userDtoUpdate)).ReturnsAsync(userDtoUpdateResult);
      _service = _serviceMock.Object;

      deletado = await _service.Delete(Guid.NewGuid());
      Assert.False(deletado);
    }

  }
}
