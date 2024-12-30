using NoteApp.Core.Domain.Enums;

namespace NoteApp.Core.Exception;

public class BaseException : System.Exception
{
    public ResponseMessage? ResponseMessage;
    
    protected BaseException()
    {
    }

    protected BaseException(ResponseMessage? responseMessage)
    {
        this.ResponseMessage = responseMessage;
    }

    protected BaseException(string message) : base(message)
    {
    }

    protected BaseException(string message, System.Exception innerException) : base(message, innerException)
    {
    }

    protected BaseException(System.Exception innerException) : base("An error occurred.", innerException)
    {
    }
}