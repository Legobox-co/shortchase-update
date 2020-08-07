using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Shortchase.Entities;
using Shortchase.Helpers;

namespace Shortchase.Authorization
{
    public class PermissionClaimsConfiguration : UserClaimsPrincipalFactory<User>
    {
        private readonly DataContext context;

        public PermissionClaimsConfiguration(UserManager<User> userManager, IOptions<IdentityOptions> optionsAccessor, DataContext context) : base(userManager, optionsAccessor)
        {
            this.context = context;
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            ClaimsIdentity identity = await base.GenerateClaimsAsync(user).ConfigureAwait(true);

            PermissionCalculator PermissionCalculator = new PermissionCalculator(context);
            string Permissions = await PermissionCalculator.CalculatePermissions(user.Id).ConfigureAwait(true);
            var PermissionClaims = identity.FindAll(PermissionConstants.ClaimType);
            foreach (var claim in PermissionClaims) identity.RemoveClaim(claim);
            identity.AddClaim(new Claim(PermissionConstants.ClaimType, Permissions));

            if (PermissionConstants.RefreshEnabled)
            {
                var RefreshClaims = identity.FindAll(PermissionConstants.RefreshClaimType);
                foreach (var claim in RefreshClaims) identity.RemoveClaim(claim);
                identity.AddClaim(new Claim(PermissionConstants.RefreshClaimType, DateTime.UtcNow.ToString("s")));
            }

            return identity;
        }
    }
}