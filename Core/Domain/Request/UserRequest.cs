using NoteApp.Data.Entity.User;

namespace NoteApp.Core.Domain.Request;

public class UserRequest
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public User ToEntity()
    {
        return new User
        {
            FullName = FullName,
            Email = Email,
            Password = BCrypt.Net.BCrypt.HashPassword(Password),
            Role = Role.User
        };
    }
}