using Microsoft.EntityFrameworkCore;
using NoteApp.Data.Entity;

namespace NoteApp.Data;

public class AppDatabaseContext(DbContextOptions<AppDatabaseContext> options) : DbContext(options)
{
    public DbSet<Note> Notes { get; set; }
}