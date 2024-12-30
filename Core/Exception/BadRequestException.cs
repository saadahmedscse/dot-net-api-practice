using NoteApp.Core.Domain.Enums;

namespace NoteApp.Core.Exception;

public class BadRequestException : BaseException
{
    public BadRequestException(string message) : base(message)
    {
    }

    public BadRequestException(ResponseMessage? responseMessage) : base(responseMessage)
    {
    }
}