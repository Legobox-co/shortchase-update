using Microsoft.AspNetCore.Authorization;

namespace Shortchase.Authorization
{
    internal class PermittedAttribute : AuthorizeAttribute
    {
        public PermittedAttribute(params Permission[] permission) : base(permission.PackPermissionsIntoString())
        {
        }
    }
}