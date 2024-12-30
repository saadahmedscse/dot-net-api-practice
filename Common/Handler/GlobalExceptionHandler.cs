using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NoteApp.Core.Domain.Enums;
using NoteApp.Core.Domain.Response;
using NoteApp.Core.Exception;

namespace NoteApp.Common.Handler;

public class GlobalExceptionHandler : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        context.Result = context.Exception switch
        {
            ApiException apiException => Response(apiException.ResponseMessage ?? ResponseMessage.INTERNAL_SERVER_ERROR,
                apiException.ResponseMessage == null ? apiException.Message : apiException.ResponseMessage.Message),
            
            BadRequestException badRequestException => Response(
                badRequestException.ResponseMessage ?? ResponseMessage.BAD_REQUEST,
                badRequestException.ResponseMessage == null
                    ? badRequestException.Message
                    : badRequestException.ResponseMessage.Message),
            
            _ => Response(ResponseMessage.INTERNAL_SERVER_ERROR, ResponseMessage.INTERNAL_SERVER_ERROR.Message)
        };

        context.ExceptionHandled = true;
    }

    private static IActionResult Response(ResponseMessage responseMessage, string message)
    {
        return new ObjectResult(
            new BaseResponse<object>(responseMessage.Code, message, null)
        ) { StatusCode = (int)responseMessage.HttpStatus };
    }
}