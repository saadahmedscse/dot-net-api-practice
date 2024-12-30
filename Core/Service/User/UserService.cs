using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NoteApp.Common.util;
using NoteApp.Core.Domain.Request;
using NoteApp.Core.Exception;
using NoteApp.Data;

namespace NoteApp.Core.Service.User;

public class UserService(AppDatabaseContext dbContext, JwtUtil jwtUtil) : BaseService, IUserService
{
    public IActionResult SignUp(UserRequest? body)
    {
        ValidateSignUpRequest(body);
        Debug.Assert(body != null);

        try
        {
            dbContext.Users.Add(body.ToEntity());
            dbContext.SaveChanges();
            
            return OperationSuccessful<object>("User has been created successfully", null);
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e);
            throw new ApiException(e);
        }
    }

    public IActionResult Login(LoginRequest? body)
    {
        ValidateLoginRequest(body);
        Debug.Assert(body != null);

        try
        {
            var accessToken = jwtUtil.GenerateAccessToken(body.Email);

            return OperationSuccessful("User has been logged in", accessToken);
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e);
            throw new ApiException(e);
        }
    }

    private void ValidateSignUpRequest(UserRequest? body)
    {
        if (body == null) throw new BadRequestException("Body is required");
        if (IsNullOrBlank(body.FullName)) throw new BadRequestException("FullName is required");
        if (IsNullOrBlank(body.Email)) throw new BadRequestException("Email is required");
        if (IsNullOrBlank(body.Password)) throw new BadRequestException("Password is required");
    }

    private void ValidateLoginRequest(LoginRequest? body)
    {
        if (body == null) throw new BadRequestException("Body is required");
        if (IsNullOrBlank(body.Email)) throw new BadRequestException("Email is required");
        var user = dbContext.Users.FirstOrDefault(u => u.Email.Equals(body.Email));
        if (user == null) throw new BadRequestException("No user found");
        if (IsNullOrBlank(body.Password)) throw new BadRequestException("Password is required");
        if (!BCrypt.Net.BCrypt.Verify(body.Password, user.Password)) throw new BadRequestException("Password is incorrect");
    }
}