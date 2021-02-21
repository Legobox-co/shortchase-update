using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Shortchase.Authorization;
using Shortchase.Entities;

namespace Shortchase.Services
{
    public partial interface IPermissionService
    {
        Task<ICollection<Permissions>> GetAll();

        Task<IQueryable<Permissions>> GetAllQ();

        Task<Permissions> GetById(int id);

        Task Insert(Permissions Permission);

        Task Update(Permissions Permission);

        Task Disable(Permissions Permission);

        Task<Permissions> GetByName(string name);

        Task<ICollection<Permissions>> GetByUser(Guid UserId);

        Task<ICollection<Permissions>> GetByUser(User user);

        Task<bool> AddToUser(User user, Permission permission);
        Task<ICollection<Permissions>> GetAdminRoles()

        Task<bool> AddToUser(Guid UserId, Permission permission);

        Task<bool> RemoveFromUser(User user, Permission permission);

        Task<bool> RemoveFromUser(Guid UserId, Permission permission);

        Task<bool> UpdateClaims(IIdentity user);

        Task<bool> HasPermission(Guid UserId, Permission permission);
    }
}