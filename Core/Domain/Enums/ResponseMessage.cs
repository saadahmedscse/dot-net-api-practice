using System.Net;

namespace NoteApp.Core.Domain.Enums;

public class ResponseMessage
{
    public string Code { get; }
    public string Message { get; }
    public HttpStatusCode HttpStatus { get; }

    private ResponseMessage(string code, string message, HttpStatusCode httpStatus)
    {
        Code = code;
        Message = message;
        HttpStatus = httpStatus;
    }

    // Define instances
    public static readonly ResponseMessage AUTH_HEADER_MISSING = new ResponseMessage(
        "E001000", "You are not authorized to access this resource.", HttpStatusCode.Unauthorized);

    public static readonly ResponseMessage AUTH_HEADER_MISMATCH = new ResponseMessage(
        "E001001", "You are not authorized to access this resource.", HttpStatusCode.Unauthorized);

    public static readonly ResponseMessage SESSION_EXPIRED = new ResponseMessage(
        "E001002", "Session expired.", HttpStatusCode.Unauthorized);

    public static readonly ResponseMessage SESSION_DATA_MISMATCH = new ResponseMessage(
        "E001003", "Unable to find user data in the system that belongs to provided session data", HttpStatusCode.Unauthorized);

    public static readonly ResponseMessage INVALID_AUTH_TOKEN = new ResponseMessage(
        "E001004", "Invalid authentication token.", HttpStatusCode.Unauthorized);

    public static readonly ResponseMessage UNAUTHORIZED_RESOURCE_ACCESS = new ResponseMessage(
        "E001005", "You are not authorized to access this resource.", HttpStatusCode.Unauthorized);

    public static readonly ResponseMessage GATEWAY_ERROR = new ResponseMessage(
        "E001006", "Unable to dispatch request at this moment. Please try after sometime", HttpStatusCode.Unauthorized);

    public static readonly ResponseMessage DECRYPTION_FAILED = new ResponseMessage(
        "E001007", "System unable to verify your request at this moment. Please try again later.", HttpStatusCode.Unauthorized);

    public static readonly ResponseMessage INACTIVE_ACCOUNT = new ResponseMessage(
        "E001008", "Your account is not active, please verify your account or contact support for further process", HttpStatusCode.Unauthorized);

    public static readonly ResponseMessage BANNED_ACCOUNT = new ResponseMessage(
        "E001009", "Your account has been banned, please contact support for further process", HttpStatusCode.Unauthorized);

    public static readonly ResponseMessage SUSPENDED_ACCOUNT = new ResponseMessage(
        "E0010010", "Your account has been suspended, please contact support for further process", HttpStatusCode.Unauthorized);

    public static readonly ResponseMessage WARNED_ACCOUNT = new ResponseMessage(
        "E001011", "Your account has been warned, please contact support for further process", HttpStatusCode.Unauthorized);

    public static readonly ResponseMessage USER_NOT_FOUND = new ResponseMessage(
        "E001012", "User not found on the system", HttpStatusCode.Unauthorized);

    public static readonly ResponseMessage BAD_REQUEST = new ResponseMessage(
        "E001013", "Bad request", HttpStatusCode.BadRequest);

    public static readonly ResponseMessage INVALID_CREDENTIALS = new ResponseMessage(
        "E001014", "Invalid credentials, password is incorrect", HttpStatusCode.Unauthorized);

    public static readonly ResponseMessage INTERNAL_SERVER_ERROR = new ResponseMessage(
        "E001015", "Internal server error", HttpStatusCode.InternalServerError);

    public static readonly ResponseMessage NOT_FOUND = new ResponseMessage(
        "E001016", "The requested resource could not be found", HttpStatusCode.NotFound);

    public static readonly ResponseMessage DISCORD_MEMBER_NOT_FOUND = new ResponseMessage(
        "E001017", "Discord member not found in the Quantum Arena Server", HttpStatusCode.Unauthorized);

    public static readonly ResponseMessage COLOR_DECODE_FAILED = new ResponseMessage(
        "E001018", "Unable to decode Hex color, maybe invalid format provided", HttpStatusCode.InternalServerError);

    public static readonly ResponseMessage OPERATION_SUCCESSFUL = new ResponseMessage(
        "S001000", "Operation successful", HttpStatusCode.OK);

    public override string ToString()
    {
        return $"{Code}: {Message} (HTTP {HttpStatus})";
    }
}
