namespace NoteApp.Common.util;

public static class HttpContextAccessor
{
    private static IHttpContextAccessor _httpContextAccessor;

    // Method to initialize the IHttpContextAccessor
    public static void Initialize(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    // Static property to access the current HttpContext
    public static HttpContext Current => _httpContextAccessor?.HttpContext;
}