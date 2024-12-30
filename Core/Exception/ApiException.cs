using NoteApp.Core.Domain.Enums;

namespace NoteApp.Core.Exception;

public class ApiException : BaseException
{
    public ApiException(System.Exception e) : base(e)
    {
    }

    public ApiException(ResponseMessage? responseMessage) : base(responseMessage)
    {
    }
}