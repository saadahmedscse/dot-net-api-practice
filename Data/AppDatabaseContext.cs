using Microsoft.EntityFrameworkCore;
using NoteApp.Data.Entity.Note;
using NoteApp.Data.Entity.User;

namespace NoteApp.Data;

public class AppDatabaseContext(DbContextOptions<AppDatabaseContext> options) : DbContext(options)
{
    public DbSet<Note> Notes { get; set; }
    
    public DbSet<User> Users { get; set; }
}