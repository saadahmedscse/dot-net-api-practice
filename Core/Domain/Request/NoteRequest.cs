using NoteApp.Data.Entity;

namespace NoteApp.Core.Domain.Request;

public class NoteRequest
{
    public string? Title { get; set; }
    public string? Content { get; set; }

    public Note ToEntity()
    {
        return new Note
        {
            Title = Title,
            Content = Content ?? ""
        };
    }
}