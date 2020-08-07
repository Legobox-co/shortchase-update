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
    public class RewardsMappingService : IRewardsMappingService
    {
        private DataContext _context;
        private readonly IErrorLogService errorLogService;

        public RewardsMappingService
        (
            DataContext context,
            IErrorLogService logService
        )
        {
            _context = context;
            this.errorLogService = logService;
        }

        public async Task<ICollection<RewardsMapping>> GetAll(bool? Available = null)
        {
            try
            {
                if (Available.HasValue)
                {
                    return await _context.RewardsMappings.Where(r => r.IsAvailable == Available.Value).ToListAsync().ConfigureAwait(false);
                }
                else {
                    return await _context.RewardsMappings.ToListAsync().ConfigureAwait(false);
                } 
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<RewardsMapping> GetById(int id)
        {
            try
            {
                return await _context.RewardsMappings.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }


        public async Task<bool> Insert(RewardsMapping mapping)
        {
            try
            {
                if (mapping != null)
                {
                    mapping.RowDate = DateTime.UtcNow;
                    mapping.IsAvailable = true;
                    _context.RewardsMappings.Add(mapping);
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


        public async Task<bool> Update(RewardsMapping mapping)
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

        public async Task<bool> Delete(int id)
        {
            try
            {
                RewardsMapping item = await _context.RewardsMappings.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
                if (item != null) {
                    var res = _context.RewardsMappings.Remove(item);
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

        public async Task<bool> Activate(int id)
        {
            try
            {
                RewardsMapping item = await _context.RewardsMappings.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
                if (item != null)
                {
                    item.IsAvailable = true;
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

        public async Task<bool> Deactivate(int id)
        {
            try
            {
                RewardsMapping item = await _context.RewardsMappings.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
                if (item != null)
                {
                    item.IsAvailable = false;
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