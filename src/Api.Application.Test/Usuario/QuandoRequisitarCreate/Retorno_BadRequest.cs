using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
namespace Api.Application.Test.Usuario.QuandoRequisitarCreate
{
  public class Retorno_BadRequest
  {
    private UsersController _controller;

    [Fact(DisplayName = "Retorna bad request")]
    public async Task Impossivel_Invocar_Controller_Create_BadRequest()
    {
      var serviceMock = new Mock<IUserService>();
      var nome = Faker.Name.FullName();
      var email = Faker.Internet.Email();

      serviceMock.Setup(m => m.Post(It.IsAny<UserDTOCreate>())).ReturnsAsync(
        new UserDTOCreateResult
        {
          Id = Guid.NewGuid(),
          Name = nome,
          Email = email,
          CreateAt = DateTime.UtcNow
        }
      );

      _controller = new UsersController(serviceMock.Object);
      _controller.ModelState.AddModelError("Name", "É um campo obrigatório");

      Mock<IUrlHelper> url = new Mock<IUrlHelper>();
      url.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost:5100");
      _controller.Url = url.Object;

      var userDtoCreate = new UserDTOCreate
      {
        Name = nome,
        Email = email
      };

      var result = await _controller.Post(userDtoCreate);
      Assert.True(result is BadRequestObjectResult);

    }
  }
}
