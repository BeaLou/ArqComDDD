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
  public class Retorno_Updated
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
      var userDtoUpdate = new UserDTOUpdate
      {
        Id = Guid.NewGuid(),
        Name = nome,
        Email = email
      };
      var result = await _controller.Put(userDtoUpdate);
      Assert.True(result is OkObjectResult);

      var resultValue = ((OkObjectResult)result).Value as UserDTOUpdateResult;
      Assert.NotNull(resultValue);
      Assert.Equal(userDtoUpdate.Name, resultValue.Name);
      Assert.Equal(userDtoUpdate.Email, resultValue.Email);

    }
  }
}
