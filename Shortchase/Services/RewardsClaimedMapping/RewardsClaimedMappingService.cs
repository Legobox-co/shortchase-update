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
    public class RewardsClaimedMappingService : IRewardsClaimedMappingService
    {
        private DataContext _context;
        private readonly IErrorLogService errorLogService;

        public RewardsClaimedMappingService
        (
            DataContext context,
            IErrorLogService logService
        )
        {
            _context = context;
            this.errorLogService = logService;
        }

        public async Task<ICollection<RewardsClaimedMapping>> GetAll()
        {
            try
            {
                return await _context.RewardsClaimedMappings.ToListAsync().ConfigureAwait(false);

            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }



        public async Task<ICollection<RewardsClaimedMapping>> GetAllFromUser(Guid userId, bool? Used = null)
        {
            try
            {
                if (Used.HasValue)
                {
                    return await _context.RewardsClaimedMappings.Where(r => r.UserId == userId).ToListAsync().ConfigureAwait(false);
                    //return await _context.RewardsClaimedMappings.Where(r => r.UserId == userId).Include(r => r.User).ToListAsync().ConfigureAwait(false);
                }
                else
                {
                    return await _context.RewardsClaimedMappings.Where(r => r.UserId == userId && r.IsUsed == Used.Value).ToListAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }


        public async Task<ICollection<RewardsClaimedMapping>> GetHistoryFromUser(Guid userId)
        {
            try
            {
                return await _context.RewardsClaimedMappings.Where(r => r.UserId == userId).OrderBy(o => o.RowDate).ToListAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<ICollection<RewardsClaimedMapping>> GetHistoryFromAllUsers()
        {
            try
            {
                return await _context.RewardsClaimedMappings.Include(i => i.User).OrderBy(r => r.User.FirstName).ThenByDescending(o => o.RowDate).ToListAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<RewardsClaimedMapping> GetById(Guid id)
        {
            try
            {
                return await _context.RewardsClaimedMappings.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }


        public async Task<bool> Insert(RewardsClaimedMapping mapping)
        {
            try
            {
                if (mapping != null)
                {
                    mapping.RowDate = DateTime.UtcNow;
                    _context.RewardsClaimedMappings.Add(mapping);
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


        public async Task<bool> Update(RewardsClaimedMapping mapping)
        {
            try
            {
                if (mapping != null)
                {
                    _context.Entry(mapping).State = EntityState.Modified;
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

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                RewardsClaimedMapping item = await _context.RewardsClaimedMappings.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
                if (item != null)
                {
                    var res = _context.RewardsClaimedMappings.Remove(item);
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