using System;
using Api.Domain.Dtos.UF;

namespace Api.Domain.Dtos.Municipio
{
  public class MunicipioDTOCompleto
  {
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public int CodIBGE { get; set; }
    public Guid UfId { get; set; }
    public UfDTO Uf { get; set; }
  }
}
