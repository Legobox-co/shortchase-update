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
    public class POTDListingService : IPOTDListingService
    {
        private DataContext _context;
        private readonly IErrorLogService errorLogService;

        public POTDListingService
        (
            DataContext context,
            IErrorLogService logService
        )
        {
            _context = context;
            this.errorLogService = logService;
        }

        public async Task<ICollection<POTDListing>> GetAll(bool? isDeleted = null)
        {
            try
            {
                if (isDeleted.HasValue) return await _context.POTDListings.Include(i => i.Category).Include(i => i.SubCategory).Include(i => i.Postedby)
                    .Include(i => i.Pick)
                    .Include(i => i.Market)
                    .Include(i => i.Tip).Where(a => a.Deleted == isDeleted).ToListAsync().ConfigureAwait(false);
                else return await _context.POTDListings.Include(i => i.Category).Include(i => i.SubCategory).Include(i => i.Postedby).ToListAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }


        public async Task<ICollection<POTDListing>> GetAllAvailable()
        {
            try
            {
                return await _context.POTDListings
                    .Include(i => i.Category)
                    .Include(i => i.SubCategory)
                    .Include(i => i.Postedby)
                    .Include(i => i.Pick)
                    .Include(i => i.Market)
                    .Include(i => i.Tip)
                    .Where(i => !i.Deleted && DateTime.UtcNow <= i.Pick.FinishTime)
                    .OrderBy(o => o.Pick.StartTime)
                    .ToListAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }


        public async Task<ICollection<POTDListing>> GetAllPredictedByUserId(Guid Id)
        {
            try
            {
                return await _context.POTDListings
                    .Include(i => i.Category)
                    .Include(i => i.SubCategory)
                    .Include(i => i.Postedby)
                    .Include(i => i.Pick)
                    .Include(i => i.Market)
                    .Include(i => i.Tip)
                    .Include(i => i.POTDListingPredictions).ThenInclude(i => i.PredictedBy)
                    .Include(i => i.POTDListingPredictions).ThenInclude(i => i.Market)
                    .Include(i => i.POTDListingPredictions).ThenInclude(i => i.Tip)
                    .Where(i => !i.Deleted && i.POTDListingPredictions.Any(u => u.POTDId == i.Id && u.PredictedById == Id))
                    .OrderByDescending(o => o.Pick.FinishTime)
                    .ToListAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }
        public async Task<POTDListing> GetById(Guid id)
        {
            try
            {
                return await _context.POTDListings.Include(i => i.Category).Include(i => i.SubCategory).Include(i => i.Postedby)
                    .Include(i => i.Pick)
                    .Include(i => i.Market)
                    .Include(i => i.Tip).Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }


        public async Task<bool> Insert(POTDListing item)
        {
            try
            {
                if (item != null)
                {
                    item.RowDate = DateTime.UtcNow;
                    _context.POTDListings.Add(item);
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


        public async Task<bool> Update(POTDListing item)
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

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                POTDListing item = await _context.POTDListings.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
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

        
        public async Task<bool> DeleteBatch(Guid[] id)
        {
            try
            {
                ICollection<POTDListing> items = await _context.POTDListings.Where(c => id.Contains(c.Id)).ToListAsync().ConfigureAwait(false);
                if (items != null && items.Count > 0)
                {
                    foreach (var item in items) {
                        item.Deleted = true;
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



        public async Task<bool> SwitchStatus(Guid id, bool newStatus)
        {
            try
            {
                POTDListing item = await _context.POTDListings.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
                if (item != null)
                {
                    item.Deleted = newStatus;
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




        public async Task<bool> SaveResult(Guid id, string result)
        {
            try
            {
                POTDListing item = await _context.POTDListings.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
                if (item != null)
                {
                    item.Result = result;
                    item.DateResultInformed = DateTime.UtcNow;

                    _context.Entry(item).State = EntityState.Modified;
                    await _context.SaveChangesAsync().ConfigureAwait(false);

                    return true;
                }
                else throw new Exception("No POTD found.");
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return false;
            }
        }



    }
}