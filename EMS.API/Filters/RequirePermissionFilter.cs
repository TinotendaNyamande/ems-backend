using EMS.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace EMS.API.Filters
{
    public class RequirePermissionFilter(string permission,IRolesRepository rolesRepository):IAsyncAuthorizationFilter
    {
        private readonly IRolesRepository _rolesRepository=rolesRepository;
        private readonly string _permission=permission;
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (user?.Identity?.IsAuthenticated != true)
            {
                context.Result = new ObjectResult(new ProblemDetails
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Title = "Access Denied",
                    Detail = "You need to be logged in to perform this action"
                })
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
                return;
            }
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(string.IsNullOrWhiteSpace(userId))
            {
                context.Result = new ObjectResult(new ProblemDetails
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Title = "Access Denied",
                    Detail = "You need to be logged in to perform this action"
                })
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
                return;
            }
            var allowed = await _rolesRepository.CanAccess( _permission,userId);
            if(!allowed)
            {
                context.Result = new ObjectResult(new ProblemDetails
                {
                    Status = StatusCodes.Status403Forbidden,
                    Title = "Access Denied",
                    Detail = "You do not have permission to perform this action."
                })
                {
                    StatusCode = StatusCodes.Status403Forbidden
                };
                return;
                                   
            }   
        }
    }
}
