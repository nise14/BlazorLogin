using BlazorLogin.Shared;
using Microsoft.AspNetCore.Mvc;

namespace BlazorLogin.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO login)
    {
        SessionDTO sessionDTO = new SessionDTO();

        if (login.Email == "admin@gmail.com" && login.Password == "admin")
        {
            sessionDTO.Name = "admin";
            sessionDTO.Email = login.Email;
            sessionDTO.Role = "Administrator";
        }
        else
        {
            sessionDTO.Name = "employe";
            sessionDTO.Email = login.Email;
            sessionDTO.Role = "Employe";
        }

        return await Task.FromResult(StatusCode(StatusCodes.Status200OK, sessionDTO));
    }
}