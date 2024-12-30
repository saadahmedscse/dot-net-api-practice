using Azure.Core;
using NoteApp.Common.util;
using NoteApp.Data;
using NoteApp.Data.Entity.User;
using PostSharp.Aspects;
using PostSharp.Serialization;
using HttpContextAccessor = NoteApp.Common.util.HttpContextAccessor;

namespace NoteApp.Common.Aspect;

[PSerializable]
public class PreAuthorized : OnMethodBoundaryAspect
{
    private bool shouldCheckAccountStatus;
    private Role[] roleList;

    public PreAuthorized(bool shouldCheckAccountStatus = true, Role[]? roleList = null)
    {
        this.shouldCheckAccountStatus = shouldCheckAccountStatus;
        this.roleList = roleList ?? [Role.User, Role.Admin];
    }

    public override void OnEntry(MethodExecutionArgs args)
    {
        var authorizationHeader = HttpContextAccessor.Current?.Request.Headers.Authorization.ToString();
        var tokenParts = authorizationHeader?.Split(" ");

        if (tokenParts?.Length != 2) throw new UnauthorizedAccessException("Invalid token");
        if (tokenParts[0] != "Bearer") throw new UnauthorizedAccessException("Invalid token");
        
        var jwtUtil = HttpContextAccessor.Current?.RequestServices.GetRequiredService<JwtUtil>();
        var dbContext = HttpContextAccessor.Current?.RequestServices.GetRequiredService<AppDatabaseContext>();

        if (jwtUtil.IsExpired(tokenParts[1])) throw new UnauthorizedAccessException("Token has expired");
        
        var email = jwtUtil.GetEmail(tokenParts[1]);
        var user = dbContext.Users.FirstOrDefault(x => x.Email == email);
        
        if (user == null) throw new UnauthorizedAccessException("Session data mismatch");
        
        if (!roleList.Contains(user.Role)) throw new UnauthorizedAccessException("You don't have permission to access this resource");
        
        HttpContextAccessor.Current.Items["User"] = user;
    }
}