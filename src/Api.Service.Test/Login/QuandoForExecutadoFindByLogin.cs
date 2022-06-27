using System;
using System.Threading.Tasks;
using Api.Domain.Dtos;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;

namespace Api.Service.Test.Login
{
  public class QuandoForExecutadoFindByLogin
  {
    private ILoginService _service;
    private Mock<ILoginService> _mockService;

    [Fact(DisplayName = "é possivel executar findByLogin")]
    public async Task Possivel_Executar_FindByLogin()
    {
      var email = Faker.Internet.Email();
      var objetoRetorno = new
      {
        authenticated = true,
        create = DateTime.UtcNow,
        expiration = DateTime.UtcNow.AddHours(8),
        accessToken = Guid.NewGuid(),
        userName = email,
        name = Faker.Name.FullName(),
        message = "Usuário Logado com sucesso"
      };

      var loginDTO = new LoginDTO
      {
        Email = email
      };

      _mockService = new Mock<ILoginService>();
      _mockService.Setup(m => m.FindByLogin(loginDTO)).ReturnsAsync(objetoRetorno);
      _service = _mockService.Object;

      var result = await _service.FindByLogin(loginDTO);
      Assert.NotNull(result);

    }
  }
}
