using Microsoft.AspNetCore.Mvc;
using NoteApp.Core.Domain.Request;

namespace NoteApp.Core.Service.Note;

public interface INoteService
{
    IActionResult CreateNote(NoteRequest? body);
    
    IActionResult UpdateNote(long id, NoteRequest? body);
    
    IActionResult DeleteNote(long id);
    
    IActionResult GetNote(long id);
    
    IActionResult GetNotes();
}