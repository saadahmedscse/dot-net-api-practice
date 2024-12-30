namespace NoteApp.Core.Domain.Response;

public class BaseResponse<T>(string code, string message, T? data)
    where T : class
{
    public string Code { get; set; } = code;
    public string Message { get; set; } = message;
    public T? Data { get; set; } = data;
}