using Microsoft.AspNetCore.Mvc;
using NoteApp.Core.Domain.Request;
using NoteApp.Core.Service.User;

namespace NoteApp.Controller;

[Route("api/[controller]")]
[ApiController]
public class UserController(IUserService service) : ControllerBase
{
    [HttpPost("sign-up")]
    public IActionResult SignUp(UserRequest? body)
    {
        return service.SignUp(body);
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest? body)
    {
        return service.Login(body);
    }
}