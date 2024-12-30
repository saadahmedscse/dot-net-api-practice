namespace NoteApp.Data.Entity.User;

public class User
{
    public long Id { get; set; }
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required Role Role { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public User()
    {
        var now = DateTime.Now;
        CreatedAt = now;
        UpdatedAt = now;
    }
}