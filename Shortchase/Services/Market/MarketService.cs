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
    public class MarketService : IMarketService
    {
        private DataContext _context;
        private readonly IErrorLogService errorLogService;

        public MarketService
        (
            DataContext context,
            IErrorLogService logService
        )
        {
            _context = context;
            this.errorLogService = logService;
        }

        public async Task<ICollection<Market>> GetAll(bool? isEnabled = null)
        {
            try
            {
                if (isEnabled.HasValue) return await _context.Markets.Include(i => i.Tips).Include(i => i.Category).Where(a => a.IsEnabled == isEnabled).ToListAsync().ConfigureAwait(false);
                else return await _context.Markets.Include(i => i.Tips).Include(i => i.Category).ToListAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<ICollection<MarketJSONDto>> GetAllByCategoryId(int id)
        {
            try
            {
                return await _context.Markets.Include(i => i.Tips).Include(i => i.Category).Where(c => c.CategoryId == id && c.IsEnabled).OrderBy(o => o.Name).Select(i => new MarketJSONDto { Id = i.Id, Name = i.Name }).ToListAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<Market> GetById(int id)
        {
            try
            {
                return await _context.Markets.Include(i => i.Tips).Include(i => i.Category).Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }


        public async Task<bool> Insert(Market item)
        {
            try
            {
                if (item != null)
                {
                    item.RowDate = DateTime.UtcNow;
                    _context.Markets.Add(item);
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


        public async Task<bool> Update(Market item)
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

        public async Task<bool> SoftDelete(int id)
        {
            try
            {
                Market item = await _context.Markets.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
                if (item != null)
                {
                    item.Deleted = true;
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
                Market item = await _context.Markets.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
                if (item != null)
                {
                    var res = _context.Markets.Remove(item);
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



        public async Task<bool> SwitchStatus(int id, bool newStatus)
        {
            try
            {
                Market item = await _context.Markets.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
                if (item != null)
                {
                    item.IsEnabled = newStatus;
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

        public async Task<bool> DeleteBatch(int[] Ids)
        {
            try
            {
                var items = await _context.Markets.Where(c => Ids.Contains(c.Id)).ToListAsync().ConfigureAwait(false);
                if (items != null && items.Count > 0)
                {
                    foreach (var item in items)
                    {
                        item.IsEnabled = false;
                        _context.Entry(item).State = EntityState.Modified;
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


    }
}