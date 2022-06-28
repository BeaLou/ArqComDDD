using Api.Domain.Dtos.CEP;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Dtos.UF;
using Api.Domain.Dtos.User;
using Api.Domain.Models;
using AutoMapper;

namespace Api.CrossCutting.Mappings
{
  public class DtoToModelProfile : Profile
  {
    public DtoToModelProfile()
    {
      #region User
      CreateMap<UserModel, UserDTO>()
          .ReverseMap();
      CreateMap<UserModel, UserDTOCreate>()
          .ReverseMap();
      CreateMap<UserModel, UserDTOUpdate>()
          .ReverseMap();
      #endregion

      #region UF
      CreateMap<UfModel, UfDTO>()
          .ReverseMap();
      #endregion

      #region Municipio
      CreateMap<MunicipioModel, MunicipioDTO>()
          .ReverseMap();
      CreateMap<MunicipioModel, MunicipioDTOCreate>()
          .ReverseMap();
      CreateMap<MunicipioModel, MunicipioDTOUpdate>()
          .ReverseMap();
      #endregion

      #region CEP
      CreateMap<CepModel, CepDTO>()
          .ReverseMap();
      CreateMap<CepModel, CepDTOCreate>()
          .ReverseMap();
      CreateMap<CepModel, CepDTOUpdate>()
          .ReverseMap();
      #endregion

    }


  }
}
