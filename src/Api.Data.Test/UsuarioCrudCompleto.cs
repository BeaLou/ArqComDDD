using System;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Implementations;
using Api.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Api.Data.Test
{
  public class UsuarioCrudCompleto : BaseTest, IClassFixture<DbTest>
  {
    private ServiceProvider _serviceProvider;

    public UsuarioCrudCompleto(DbTest dbTeste)
    {
      _serviceProvider = dbTeste.ServiceProvider;
    }

    [Fact(DisplayName = "Crud de Usuário")]
    [Trait("CRUD", "UserEntity")]
    public async Task E_Possivel_Realizar_Crud_Usuario()
    {
      using (var context = _serviceProvider.GetService<MyContext>())
      {
        UserImplementation _repositorio = new UserImplementation(context);
        UserEntity _entity = new UserEntity
        {
          Email = Faker.Internet.Email(),
          Name = Faker.Name.FullName(),
        };

        //Insert
        var _registroCriado = await _repositorio.InsertAsync(_entity);
        Assert.NotNull(_registroCriado);
        Assert.Equal(_entity.Email, _registroCriado.Email);
        Assert.Equal(_entity.Name, _registroCriado.Name);
        Assert.False(_registroCriado.Id == Guid.Empty);

        //Update
        _entity.Name = Faker.Name.First();
        var _registroAtualizado = await _repositorio.UpdateAsync(_entity);
        Assert.NotNull(_entity);
        Assert.Equal(_entity.Email, _registroAtualizado.Email);
        Assert.Equal(_entity.Name, _registroAtualizado.Name);

        //Exist
        var _registroExiste = await _repositorio.ExistAsync(_registroAtualizado.Id);
        Assert.True(_registroExiste);

        //Select
        var _registroSelecionado = await _repositorio.SelectAsync(_registroAtualizado.Id);
        Assert.NotNull(_registroSelecionado);
        Assert.Equal(_registroAtualizado.Email, _registroSelecionado.Email);
        Assert.Equal(_registroAtualizado.Name, _registroSelecionado.Name);

        //SelectAll
        var _todosRegistros = await _repositorio.SelectAsync();
        Assert.NotNull(_todosRegistros);
        Assert.True(_todosRegistros.Count() > 1);

        //Delete
        var _removeu = await _repositorio.DeleteAsync(_registroSelecionado.Id);
        Assert.True(_removeu);

        //Usuario Padrão
        var _usuarioPadrao = await _repositorio.FindByLogin("bia@teste.com");
        Assert.NotNull(_usuarioPadrao);
        Assert.Equal("bia@teste.com", _usuarioPadrao.Email);
        Assert.Equal("Administrator", _usuarioPadrao.Name);
      }
    }
  }
}
