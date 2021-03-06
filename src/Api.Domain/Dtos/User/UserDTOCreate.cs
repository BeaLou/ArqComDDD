using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos.User
{
  public class UserDTOCreate
  {
    public string Email { get; set; }

    [Required(ErrorMessage = "Nome é um campo obrigatorio")]
    [StringLength(60, ErrorMessage = "Nome deve ter no max. 60 caracteres")]
    [EmailAddress(ErrorMessage = "E-mail em formato inválido")]
    public string Name { get; set; }
  }
}
