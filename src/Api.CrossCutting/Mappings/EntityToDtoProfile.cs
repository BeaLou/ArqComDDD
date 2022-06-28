using Api.Domain.Dtos.CEP;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Dtos.UF;
using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using AutoMapper;

namespace Api.CrossCutting.Mappings
{
  public class EntityToDtoProfile : Profile
  {
    public EntityToDtoProfile()
    {
      CreateMap<UserDTO, UserEntity>()
         .ReverseMap();

      CreateMap<UserDTOCreateResult, UserEntity>()
         .ReverseMap();

      CreateMap<UserDTOUpdateResult, UserEntity>()
         .ReverseMap();

      CreateMap<UfDTO, UfEntity>()
         .ReverseMap();

      CreateMap<MunicipioDTO, MunicipioEntity>()
         .ReverseMap();

      CreateMap<MunicipioDTOCompleto, MunicipioEntity>()
         .ReverseMap();

      CreateMap<MunicipioDTOCreateResult, MunicipioEntity>()
         .ReverseMap();

      CreateMap<MunicipioDTOUpdateResult, MunicipioEntity>()
         .ReverseMap();

      CreateMap<CepDTO, CepEntity>()
         .ReverseMap();

      CreateMap<CepDTOCreateResult, CepEntity>()
         .ReverseMap();

      CreateMap<CepDTOUpdateResult, CepEntity>()
         .ReverseMap();

    }

  }
}
