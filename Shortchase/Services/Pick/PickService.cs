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
    public class PickService : IPickService
    {
        private DataContext _context;
        private readonly IErrorLogService errorLogService;

        public PickService
        (
            DataContext context,
            IErrorLogService logService
        )
        {
            _context = context;
            this.errorLogService = logService;
        }

        public async Task<ICollection<Pick>> GetAll(bool? isEnabled = null)
        {
            try
            {
                if (isEnabled.HasValue) return await _context.Picks.Include(i => i.Category).Where(a => a.IsEnabled == isEnabled).ToListAsync().ConfigureAwait(false);
                else return await _context.Picks.Include(i => i.Category).ToListAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }
        
        public async Task<ICollection<PickJSONDto>> GetAllByCategoryId(int id, int TimezoneOffset )
        {
            try
            {
                return await _context.Picks.Where(a => a.IsEnabled && a.CategoryId == id && a.FinishTime >= DateTime.UtcNow).OrderBy(o => o.StartTime).Select(i => new PickJSONDto { Id = i.Id, Team1 = i.Team1, Team2 = i.Team2, StartTime = DateHelper.DateFormat(i.StartTime.FromUTCData(TimezoneOffset), false), FinishTime = DateHelper.DateFormat(i.FinishTime.FromUTCData(TimezoneOffset), false) }).ToListAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<Pick> GetById(int id)
        {
            try
            {
                return await _context.Picks.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }


        public async Task<bool> Insert(Pick item)
        {
            try
            {
                if (item != null)
                {
                    item.RowDate = DateTime.UtcNow;
                    _context.Picks.Add(item);
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


        public async Task<bool> Update(Pick item)
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
                Pick item = await _context.Picks.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
                if (item != null)
                {
                    var res = _context.Picks.Remove(item);
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
                Pick item = await _context.Picks.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
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

        public async Task<bool> SwitchStatus(int id, bool newStatus)
        {
            try
            {
                Pick item = await _context.Picks.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
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
                var items = await _context.Picks.Where(c => Ids.Contains(c.Id)).ToListAsync().ConfigureAwait(false);
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

        public async  Task<List<Pick>> GetAll()
        {
            try
            {
              return await _context.Picks.ToListAsync();
               
            }
            catch (Exception e)
            {

                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public Task<List<Pick>> GetAll(object p)
        {
            throw new NotImplementedException();
        }
    }
}