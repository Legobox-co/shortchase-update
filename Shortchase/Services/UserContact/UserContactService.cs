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
    public class UserContactService : IUserContactService
    {
        private DataContext _context;
        private readonly IErrorLogService errorLogService;

        public UserContactService
        (
            DataContext context,
            IErrorLogService logService
        )
        {
            _context = context;
            this.errorLogService = logService;
        }

        public async Task<ICollection<UserContact>> GetAll()
        {
            try
            {
                return await _context.UserContacts.ToListAsync().ConfigureAwait(false);
                
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<ICollection<UserContact>> GetAllByUserId(Guid UserId, string Origin = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Origin))
                    return await _context.UserContacts.Where(i => i.UserId == UserId).ToListAsync().ConfigureAwait(false);
                else
                    return await _context.UserContacts.Where(i => i.UserId == UserId && i.Origin == Origin).ToListAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }


        public async Task<UserContact> GetById(int id)
        {
            try
            {
                return await _context.UserContacts.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }



        public async Task<bool> Insert(UserContact item)
        {
            try
            {
                if (item != null)
                {
                    item.RowDate = DateTime.Now;
                    _context.UserContacts.Add(item);
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
        public async Task<bool> InsertBatch(ICollection<UserContact> items)
        {
            try
            {
                if (items != null && items.Count > 0)
                {
                    foreach (var item in items) {
                        item.RowDate = DateTime.UtcNow;
                        _context.UserContacts.Add(item);
                    }
                    
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


        public async Task<bool> Update(UserContact item)
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



    }
}