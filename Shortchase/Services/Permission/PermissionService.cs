using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Shortchase.Authorization;
using Shortchase.Entities;
using Shortchase.Helpers;
using Shortchase.Helpers.Extensions;

namespace Shortchase.Services
{
    public partial class PermissionService : IPermissionService
    {
        private readonly DataContext db;
        private readonly IErrorLogService errorLogService;
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public PermissionService(
            DataContext context, 
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            IErrorLogService errorLogService
        )
        {
            this.db = context;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.errorLogService = errorLogService;
        }

        public async Task<bool> AddToUser(User user, Permission permission)
        {
            try
            {
                if (user == null) throw new Exception("User cannot be null");

                return await AddToUser(user.Id, permission).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                if (!(await errorLogService.InsertException(e).ConfigureAwait(false))) throw;
                throw;
            }
}

        public async Task<bool> AddToUser(Guid UserId, Permission permission)
        {
            try
            {
                User User = await db.Users.Where(u => u.Id == UserId).SingleOrDefaultAsync().ConfigureAwait(false);
                if (User == null) throw new Exception("User cannot be null");
                Permissions Permission = await db.Permissions.Where(p => (Permission)p.Value == permission).SingleOrDefaultAsync().ConfigureAwait(false);
                if (Permission == null) 
                {
                    Permission = new Permissions
                    {
                        Description = "",
                        Disabled = false,
                        GroupName = "",
                        Name = Enum.GetName(typeof(Permission), permission),
                        RowDate = DateTime.Now,
                        Value = (ushort)permission
                    };
                    await Insert(Permission).ConfigureAwait(false);
                }

                UserPermissions UserPermissions = new UserPermissions
                {
                    
                    Permissions = Permission,
                    PermissionsId = Permission.Id,
                    RowDate = DateTime.Now,
                    User = User,
                    UserId = UserId
                };

                await db.UserPermissions.AddAsync(UserPermissions).ConfigureAwait(false);
                await db.SaveChangesAsync().ConfigureAwait(false);
                return true;
        }
            catch (Exception e)
            {
                if (!(await errorLogService.InsertException(e).ConfigureAwait(false))) throw;
                return false;
            }
}

        public async Task Disable(Permissions Permission)
        {
            try
            {
                if (Permission == null) throw new Exception(nameof(Permission) + " cannot be null");
                Permission.Disabled = true;
                db.Entry(Permission).State = EntityState.Modified;
                await db.SaveChangesAsync().ConfigureAwait(true);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<ICollection<Permissions>> GetAdminRoles()
        {
            try
            {
                //string owner = "Admin";
                //string admin = "SuperAdmin";

                return await db.Permissions.FromSqlRaw("select * from Permissions where GroupName = 'Admin' or GroupName = 'SuperAdmin'").ToListAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<ICollection<Permissions>> GetAll()
        {
            try
            {
                return await db.Permissions.ToListAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<IQueryable<Permissions>> GetAllQ()
        {
            try
            {
                return db.Permissions;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<Permissions> GetById(int id)
        {
            try
            {
                return await db.Permissions.FindAsync(id);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<Permissions> GetByName(string name)
        {
            try
            {
                return await db.Permissions.Where(a => a.Name.ToUpperInvariant() == name.ToUpperInvariant()).SingleOrDefaultAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<ICollection<Permissions>> GetByUser(Guid UserId)
        {
            try
            {
                return await db.Users.Where(u => u.Id == UserId).SelectMany(a => a.Permissions.Select(b => b.Permissions)).ToListAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<ICollection<Permissions>> GetByUser(User user)
        {
            try
            {
                return await db.Users.Where(u => u.Id == user.Id).SelectMany(a => a.Permissions.Select(b => b.Permissions)).ToListAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task Insert(Permissions Permission)
        {
            try
            {
                if (Permission == null) throw new Exception("Permission should not be null");

                Permission.RowDate = DateTime.Now;
                await db.Permissions.AddAsync(Permission);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<bool> RemoveFromUser(User user, Permission permission)
        {
            try
            {
                if (user == null) throw new Exception("User should not be null");
                return await RemoveFromUser(user.Id, permission).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                if (!(await errorLogService.InsertException(e).ConfigureAwait(false))) throw;
                return false;
            }
        }

        public async Task<bool> RemoveFromUser(Guid UserId, Permission permission)
        {
            try
            {
                User User = await db.Users.Where(u => u.Id == UserId).Include(p => p.Permissions).ThenInclude(p => p.Permissions).SingleOrDefaultAsync().ConfigureAwait(false);
                var Permissions = User.Permissions.Where(p => (Permission)p.Permissions.Value == permission).ToList();
                db.UserPermissions.RemoveRange(Permissions);
                await db.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }
            catch (Exception e)
            {
                if (!(await errorLogService.InsertException(e).ConfigureAwait(false))) throw;
                return false;
            }
        }

        public async Task Update(Permissions Permission)
        {
            try
            {
                if (Permission == null) throw new Exception("Cannot update null permission");

                db.Entry(Permission).State = EntityState.Modified;
                await db.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<bool> UpdateClaims(IIdentity user)
        {
            User CurrentUser = await userManager.FindByIdAsync(user.Id()).ConfigureAwait(true);
            if (CurrentUser == null) return false;
            if (user == null) return false;
            if (!(user is ClaimsIdentity identity)) return false;

            PermissionCalculator PermissionCalculator = new PermissionCalculator(db);
            string Permissions = await PermissionCalculator.CalculatePermissions(new Guid(identity.Id())).ConfigureAwait(true);

            var PermissionClaims = identity.FindAll(PermissionConstants.ClaimType);
            foreach (var claim in PermissionClaims.ToList()) identity.RemoveClaim(claim);

            identity.AddClaim(new Claim(PermissionConstants.ClaimType, Permissions));

            await signInManager.RefreshSignInAsync(CurrentUser).ConfigureAwait(true);
            return true;
        }

        public async Task<bool> HasPermission(Guid UserId, Permission permission)
        {
            try
            {
                return await db.UserPermissions.Include(a => a.Permissions).Where(a => a.UserId == UserId && a.Permissions.Value == (ushort)permission).AnyAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }
    }
}