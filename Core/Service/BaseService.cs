using Microsoft.AspNetCore.Mvc;
using NoteApp.Core.Domain.Enums;
using NoteApp.Core.Domain.Response;

namespace NoteApp.Core.Service;

public abstract class BaseService
{
    private IActionResult Response<T>(ResponseMessage responseMessage, string message, T? data) where T : class
    {
        return new ObjectResult(
            new BaseResponse<T>(responseMessage.Code, message, data)
        ) { StatusCode = (int) responseMessage.HttpStatus };
    }

    protected IActionResult OperationSuccessful<T>(string message, T? data) where T : class
    {
        return Response(ResponseMessage.OPERATION_SUCCESSFUL, message, data);
    }

    protected bool IsNullOrBlank(string? value)
    {
        return string.IsNullOrEmpty(value);
    }
}