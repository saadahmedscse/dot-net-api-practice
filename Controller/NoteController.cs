using Microsoft.AspNetCore.Mvc;
using NoteApp.Core.Domain.Request;
using NoteApp.Core.Service.Note;
using NoteApp.Data.Entity;

namespace NoteApp.Controller;

[Route("api/[controller]")]
[ApiController]
public class NoteController(INoteService service) : ControllerBase
{
    
    [HttpGet]
    public IActionResult GetAll()
    {
        return service.GetNotes();
    }

    [HttpPost]
    public IActionResult Add(NoteRequest? body)
    {
        return service.CreateNote(body);
    }

    [HttpPut("{id:long}")]
    public IActionResult Update(long id, NoteRequest? body)
    {
        return service.UpdateNote(id, body);
    }

    [HttpDelete("{id:long}")]
    public IActionResult Delete(long id)
    {
        return service.DeleteNote(id);
    }

    [HttpGet("{id:long}")]
    public IActionResult Get(long id)
    {
        return service.GetNote(id);
    }
}