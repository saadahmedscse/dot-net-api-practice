using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteApp.Common.Aspect;
using NoteApp.Core.Domain.Request;
using NoteApp.Core.Service.Note;
using NoteApp.Data.Entity.User;

namespace NoteApp.Controller;

[Route("api/[controller]")]
[ApiController]
public class NoteController(INoteService service) : ControllerBase
{
    [HttpGet]
    [PreAuthorized]
    public IActionResult GetAll()
    {
        return service.GetNotes();
    }

    [HttpPost]
    [PreAuthorized]
    public IActionResult Add(NoteRequest? body)
    {
        return service.CreateNote(body);
    }

    [HttpPut("{id:long}")]
    [PreAuthorized]
    public IActionResult Update(long id, NoteRequest? body)
    {
        return service.UpdateNote(id, body);
    }

    [HttpDelete("{id:long}")]
    [PreAuthorized(roleList: [Role.Admin])]
    public IActionResult Delete(long id)
    {
        return service.DeleteNote(id);
    }

    [HttpGet("{id:long}")]
    [PreAuthorized]
    public IActionResult Get(long id)
    {
        return service.GetNote(id);
    }
}