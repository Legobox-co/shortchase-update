using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Shortchase.Helpers;

namespace Shortchase.Authorization
{
    public class PermissionCalculator
    {
        private readonly DataContext context;

        public PermissionCalculator(DataContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Get all User Permissions and packages them into a string
        /// </summary>
        /// <param name="UserId">Id of the user</param>
        /// <returns></returns>
        public async Task<string> CalculatePermissions(Guid UserId)
        {
            var userPermissions = await context.UserPermissions
                .Where(u => u.UserId == UserId)
                .Select(x => x.Permissions)
                .ToListAsync()
                .ConfigureAwait(true);

            var Permissions = userPermissions.Select(a => a.Enum).Where(b => b.HasValue).Select(a => a.Value);

            return Permissions.PackPermissionsIntoString();
        }
    }
}