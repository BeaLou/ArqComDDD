using System.Net;
using Api.Domain.Dtos;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class LoginController : ControllerBase
  {
    [HttpPost]
    [AllowAnonymous]
    public async Task<object> Login([FromBody] LoginDTO loginDto, [FromServices] ILoginService service)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      if (loginDto == null)
      {
        return BadRequest();
      }

      try
      {
        var result = await service.FindByLogin(loginDto);

        if (result != null)
        {
          return Ok(result);
        }
        else
        {
          return NotFound();
        }
      }
      catch (ArgumentException e)
      {
        return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
      }
    }
  }
}
