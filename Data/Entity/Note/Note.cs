namespace NoteApp.Data.Entity.Note;

public class Note
{
    public long Id { get; set; }
    public string? Title { get; set; }
    public required string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public Note()
    {
        var now = DateTime.Now;
        CreatedAt = now;
        UpdatedAt = now;
    }
}