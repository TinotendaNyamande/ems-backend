using Microsoft.AspNetCore.Mvc;

namespace EMS.API.Filters
{
    public class RequirePermissionAttribute : TypeFilterAttribute
    {
        public RequirePermissionAttribute(string permission):base(typeof(RequirePermissionFilter))
        {
            Arguments=new object[] { permission };
        }
    }
}
