// Copyright (c) 2019 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using System.Threading.Tasks;
using Shortchase.Services;

namespace Shortchase.Authorization
{
    //thanks to https://www.jerriepelser.com/blog/creating-dynamic-authorization-policies-aspnet-core/

    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IPermissionService permissionService;
        private readonly IErrorLogService errorLogService;
        public PermissionHandler(IPermissionService permissionService, IErrorLogService errorLogService)
        {
            this.permissionService = permissionService;
            this.errorLogService = errorLogService;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            try
            {
                var permissionsClaim = context.User.Claims.SingleOrDefault(c => c.Type == PermissionConstants.ClaimType);
                // If user does not have the scope claim, end challenge
                if (permissionsClaim == null) return;

                if (PermissionConstants.RefreshEnabled)
                {
                    var permissionsRefreshClaim = context.User.Claims.SingleOrDefault(c => c.Type == PermissionConstants.RefreshClaimType);
                    // If refresh is enabled but user doesn't have a refresh claim, end challenge
                    if (permissionsRefreshClaim == null) return;
                    DateTime RefreshDate = DateTime.Parse(permissionsRefreshClaim.Value).AddSeconds(PermissionConstants.RefreshTime);
                    DateTime CurrentDate = DateTime.UtcNow;
                    if (RefreshDate < CurrentDate)
                    {
                        await permissionService.UpdateClaims(context.User.Identity).ConfigureAwait(true);
                        permissionsClaim = context.User.Claims.SingleOrDefault(c => c.Type == PermissionConstants.ClaimType);
                        if (permissionsClaim == null) return;
                    }
                }

                if (permissionsClaim.Value.ThisPermissionIsAllowed(requirement.PermissionName)) context.Succeed(requirement);

                return;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return;
            }
        }
    }
}