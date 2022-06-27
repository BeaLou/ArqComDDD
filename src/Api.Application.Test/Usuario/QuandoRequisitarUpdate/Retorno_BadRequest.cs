using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Usuario.QuandoRequisitarUpdate
{
  public class Retorno_BadRequest
  {
    private UsersController _controller;

    [Fact(DisplayName = "Possivel realizar created")]
    public async Task Possivel_Invocar_Controller_Create()
    {
      var serviceMock = new Mock<IUserService>();
      var nome = Faker.Name.FullName();
      var email = Faker.Internet.Email();

      serviceMock.Setup(m => m.Put(It.IsAny<UserDTOUpdate>())).ReturnsAsync(
        new UserDTOUpdateResult
        {
          Id = Guid.NewGuid(),
          Name = nome,
          Email = email,
          UpdateAt = DateTime.UtcNow
        }
      );
      _controller = new UsersController(serviceMock.Object);
      _controller.ModelState.AddModelError("Email", "é um campo obrigatorio");

      var userDtoUpdate = new UserDTOUpdate
      {
        Id = Guid.NewGuid(),
        Name = nome,
        Email = email
      };
      var result = await _controller.Put(userDtoUpdate);
      Assert.True(result is BadRequestObjectResult);
      Assert.False(_controller.ModelState.IsValid);
    }
  }
}
