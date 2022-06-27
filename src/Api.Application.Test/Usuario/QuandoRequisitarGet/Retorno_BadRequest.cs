using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
namespace Api.Application.Test.Usuario.QuandoRequisitarGet
{
  public class Retorno_BadRequest
  {
    private UsersController _controller;

    [Fact(DisplayName = "Possivel realizar get")]
    public async Task Impossivel_Invocar_Controller_Get_BadRequest()
    {
      var serviceMock = new Mock<IUserService>();
      var nome = Faker.Name.FullName();
      var email = Faker.Internet.Email();

      serviceMock.Setup(m => m.Get(It.IsAny<Guid>())).ReturnsAsync(
        new UserDTO
        {
          Id = Guid.NewGuid(),
          Name = nome,
          Email = email,
          CreateAt = DateTime.UtcNow
        });

      _controller = new UsersController(serviceMock.Object);
      _controller.ModelState.AddModelError("Id", "Formato Inválido");

      var result = await _controller.Get(Guid.NewGuid());
      Assert.True(result is BadRequestObjectResult);

    }
  }
}
