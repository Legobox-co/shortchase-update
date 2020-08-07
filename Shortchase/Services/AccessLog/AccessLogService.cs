using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Shortchase.Dtos;
using Shortchase.Entities;
using Shortchase.Helpers;
using Shortchase.Authorization;

namespace Shortchase.Services
{
    public class AccessLogService : IAccessLogService
    {
        private DataContext _context;
        private readonly IErrorLogService errorLogService;

        public AccessLogService
        (
            DataContext context,
            IErrorLogService logService
        )
        {
            _context = context;
            this.errorLogService = logService;
        }

        public async Task<ICollection<AccessLog>> GetAll()
        {
            try
            {
                return await _context.AccessLogs.Include(i => i.User).OrderByDescending(o => o.RowDate).ToListAsync().ConfigureAwait(false);
                
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }



        public async Task<AccessLog> GetLatestByUserId(Guid id)
        {
            try
            {
                return await _context.AccessLogs.Include(i => i.User).Where(c => c.UserId == id).OrderByDescending(o => o.RowDate).FirstOrDefaultAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<AccessLog> GetById(int id)
        {
            try
            {
                return await _context.AccessLogs.Include(i => i.User).Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }


        public async Task<bool> Insert(AccessLog item)
        {
            try
            {
                if (item != null)
                {
                    item.RowDate = DateTime.UtcNow;
                    _context.AccessLogs.Add(item);
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                }
                return true;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return false;
            }
        }

        public async Task<bool> Insert(Guid UserId, string Type, string Ip)
        {
            try
            {
                if (UserId == Guid.Empty) throw new Exception("No user found.");
                AccessLog item = new AccessLog { 
                    UserId = UserId,
                    Type = Type,
                    RowDate = DateTime.UtcNow,
                    IP = Ip
                };
                _context.AccessLogs.Add(item);
                await _context.SaveChangesAsync().ConfigureAwait(false);

                return true;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return false;
            }
        }


        public async Task<bool> Update(AccessLog item)
        {
            try
            {
                if (item != null)
                {
                    _context.Entry(item).State = EntityState.Modified;
                    await _context.SaveChangesAsync().ConfigureAwait(false);

                }
                return true;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                AccessLog item = await _context.AccessLogs.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
                if (item != null)
                {
                    var res = _context.AccessLogs.Remove(item);
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                }
                return true;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return false;
            }
        }




        public async Task<bool> IsAnyAdminOnline()
        {
            try
            {
                bool isOnline = false;
                var adminPermission = await _context.Permissions.Where(i => i.Name.ToUpper() == "ADMIN").SingleOrDefaultAsync().ConfigureAwait(false);
                var usersWithAdminPermission = await _context.UserPermissions.Where(i => i.PermissionsId == adminPermission.Id).Select(i => i.UserId).ToListAsync().ConfigureAwait(false);
                var adminLastLogin = await _context.AccessLogs.Where(i => usersWithAdminPermission.Contains(i.UserId)).OrderByDescending(o => o.RowDate).FirstOrDefaultAsync().ConfigureAwait(false);
                if (adminLastLogin != null) {
                    TimeSpan diff = DateTime.UtcNow - adminLastLogin.RowDate;
                    if (diff.TotalMinutes < 3)
                    {
                        isOnline = true;
                    }
                }
                return isOnline;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }


    }
}