using System;
using System.Collections.Generic;
using Api.Domain.Dtos.User;
using Faker;
namespace Api.Service.Test.Usuario
{
  public class UsuarioTestes
  {
    public static string NomeUsuario { get; set; }
    public static string EmailUsuario { get; set; }
    public static string NomeUsuarioAlterado { get; set; }
    public static string EmailUsuarioAlterado { get; set; }
    public static Guid IdUsuario { get; set; }

    public List<UserDTO> listaUserDTO = new List<UserDTO>();
    public UserDTO userDto;
    public UserDTOCreate userDtoCreate;
    public UserDTOCreateResult userDtoCreateResult;
    public UserDTOUpdate userDtoUpdate;
    public UserDTOUpdateResult userDtoUpdateResult;

    public UsuarioTestes()
    {
      IdUsuario = Guid.NewGuid();
      NomeUsuario = Faker.Name.FullName();
      EmailUsuario = Faker.Internet.Email();
      NomeUsuarioAlterado = Faker.Name.FullName();
      EmailUsuarioAlterado = Faker.Internet.Email();

      for (int i = 0; i < 10; i++)
      {
        var dto = new UserDTO()
        {
          Id = Guid.NewGuid(),
          Name = Faker.Name.FullName(),
          Email = Faker.Internet.Email()
        };
        listaUserDTO.Add(dto);
      }

      userDto = new UserDTO
      {
        Id = IdUsuario,
        Name = NomeUsuario,
        Email = EmailUsuario
      };

      userDtoCreate = new UserDTOCreate
      {
        Name = NomeUsuario,
        Email = EmailUsuario
      };
      userDtoCreateResult = new UserDTOCreateResult
      {
        Id = IdUsuario,
        Name = NomeUsuario,
        Email = EmailUsuario,
        CreateAt = DateTime.UtcNow
      };

      userDtoUpdate = new UserDTOUpdate
      {
        Id = IdUsuario,
        Name = NomeUsuario,
        Email = EmailUsuario
      };
      userDtoUpdateResult = new UserDTOUpdateResult
      {
        Id = IdUsuario,
        Name = NomeUsuario,
        Email = EmailUsuario,
        UpdateAt = DateTime.UtcNow
      };

    }
  }
}
