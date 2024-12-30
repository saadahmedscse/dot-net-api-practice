using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NoteApp.Core.Domain.Request;
using NoteApp.Core.Exception;
using NoteApp.Data;

namespace NoteApp.Core.Service.Note;

public class NoteService(AppDatabaseContext databaseContext) : BaseService, INoteService
{
    public IActionResult CreateNote(NoteRequest? body)
    {
        ValidateCreateNoteRequest(body);
        Debug.Assert(body != null);
        
        var note = body.ToEntity();

        try
        {
            var createdNote = databaseContext.Notes.Add(note).Entity;
            databaseContext.SaveChanges();
            
            return OperationSuccessful("Note has been added successfully", createdNote);
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e);
            throw new ApiException(e);
        }
    }

    public IActionResult UpdateNote(long id, NoteRequest? body)
    {
        var note = databaseContext.Notes.Find(id);
        if (note == null)
        {
            throw new BadRequestException("No note found with the id " + id);
        }
        
        if (body == null) throw new BadRequestException("Note body is null");
        if (!body.Title.IsNullOrEmpty()) note.Title = body.Title;
        if (!body.Content.IsNullOrEmpty()) note.Content = body.Content ?? "";
        note.UpdatedAt = DateTime.Now;

        try
        {
            databaseContext.Notes.Update(note);
            databaseContext.SaveChanges();
            
            return OperationSuccessful("Note has been updated successfully", note);
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e);
            throw new ApiException(e);
        }
    }

    public IActionResult DeleteNote(long id)
    {
        var note = databaseContext.Notes.Find(id);
        if (note == null)
        {
            throw new BadRequestException("No note found with the id " + id);
        }
        
        databaseContext.Notes.Remove(note);
        databaseContext.SaveChanges();
        
        return OperationSuccessful<object>("Note with id " + id + " has been deleted", null);
    }

    public IActionResult GetNote(long id)
    {
        var note = databaseContext.Notes.Find(id);
        if (note == null)
        {
            throw new BadRequestException("No note found with the id " + id);
        }

        return OperationSuccessful("Note has been retrieved successfully", note);
    }

    public IActionResult GetNotes()
    {
        var notes = databaseContext.Notes.ToList();

        return OperationSuccessful("All notes has been retrieved successfully", notes);
    }

    private void ValidateCreateNoteRequest(NoteRequest? body)
    {
        if (body == null) throw new BadRequestException("Note body is null");
        if (body.Title == null) throw new BadRequestException("Note title is null");
        if (body.Content == null) throw new BadRequestException("Note content is null");
    }
}