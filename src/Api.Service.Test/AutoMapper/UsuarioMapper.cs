using System;
using System.Collections.Generic;
using System.Linq;
using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using Api.Domain.Models;
using Xunit;

namespace Api.Service.Test.AutoMapper
{
  public class UsuarioMapper : BaseTestService
  {
    [Fact(DisplayName = "é possivel mapear os modelos")]
    public void E_Possivel_Mapear_Modelos()
    {
      var model = new UserModel
      {
        Id = Guid.NewGuid(),
        Name = Faker.Name.FullName(),
        Email = Faker.Internet.Email(),
        CreateAt = DateTime.UtcNow,
        UpdateAt = DateTime.UtcNow
      };

      var listaEntity = new List<UserEntity>();
      for (int i = 0; i < 5; i++)
      {
        var item = new UserEntity
        {
          Id = Guid.NewGuid(),
          Name = Faker.Name.FullName(),
          Email = Faker.Internet.Email(),
          CreateAt = DateTime.UtcNow,
          UpdateAt = DateTime.UtcNow
        };
        listaEntity.Add(item);
      }
      //ModelToEntity
      var entity = Mapper.Map<UserEntity>(model);
      Assert.Equal(entity.Id, model.Id);
      Assert.Equal(entity.Name, model.Name);
      Assert.Equal(entity.Email, model.Email);
      Assert.Equal(entity.CreateAt, model.CreateAt);
      Assert.Equal(entity.UpdateAt, model.UpdateAt);

      //EntityToDTO
      var userDTO = Mapper.Map<UserDTO>(entity);
      Assert.Equal(userDTO.Id, entity.Id);
      Assert.Equal(userDTO.Name, entity.Name);
      Assert.Equal(userDTO.Email, entity.Email);
      Assert.Equal(userDTO.CreateAt, entity.CreateAt);

      var listUserDTO = Mapper.Map<List<UserDTO>>(entity);
      Assert.True(listUserDTO.Count() == listaEntity.Count());

      for (int i = 0; i < listUserDTO.Count(); i++)
      {
        Assert.Equal(listUserDTO[i].Id, listaEntity[i].Id);
        Assert.Equal(listUserDTO[i].Name, listaEntity[i].Name);
        Assert.Equal(listUserDTO[i].Email, listaEntity[i].Email);
        Assert.Equal(listUserDTO[i].CreateAt, listaEntity[i].CreateAt);
      }
      var userDtoCreateResult = Mapper.Map<UserDTOCreateResult>(entity);
      Assert.Equal(userDtoCreateResult.Id, entity.Id);
      Assert.Equal(userDtoCreateResult.Name, entity.Name);
      Assert.Equal(userDtoCreateResult.Email, entity.Email);
      Assert.Equal(userDtoCreateResult.CreateAt, entity.CreateAt);

      var userDtoUpdateResult = Mapper.Map<UserDTOUpdateResult>(entity);
      Assert.Equal(userDtoUpdateResult.Id, entity.Id);
      Assert.Equal(userDtoUpdateResult.Name, entity.Name);
      Assert.Equal(userDtoUpdateResult.Email, entity.Email);
      Assert.Equal(userDtoUpdateResult.UpdateAt, entity.UpdateAt);

      //DtoToModel
      var userModel = Mapper.Map<UserModel>(userDTO);
      Assert.Equal(userModel.Id, userDTO.Id);
      Assert.Equal(userModel.Name, userDTO.Name);
      Assert.Equal(userModel.Email, userDTO.Email);
      Assert.Equal(userModel.CreateAt, userDTO.CreateAt);

      var userDtoCreate = Mapper.Map<UserDTOCreate>(userModel);
      Assert.Equal(userDtoCreate.Name, userModel.Name);
      Assert.Equal(userDtoCreate.Email, userModel.Email);

      var userDtoUpdate = Mapper.Map<UserDTOUpdate>(userModel);
      Assert.Equal(userDtoUpdate.Id, userModel.Id);
      Assert.Equal(userDtoUpdate.Name, userModel.Name);
      Assert.Equal(userDtoUpdate.Email, userModel.Email);

    }
  }
}
