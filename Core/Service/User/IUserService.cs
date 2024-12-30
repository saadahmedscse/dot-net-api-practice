using Microsoft.AspNetCore.Mvc;
using NoteApp.Core.Domain.Request;

namespace NoteApp.Core.Service.User;

public interface IUserService
{
    IActionResult SignUp(UserRequest? body);
    
    IActionResult Login(LoginRequest? body);
}