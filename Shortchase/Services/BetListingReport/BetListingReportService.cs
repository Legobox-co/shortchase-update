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
    public class BetListingReportService : IBetListingReportService
    {
        private DataContext _context;
        private readonly IErrorLogService errorLogService;

        public BetListingReportService
        (
            DataContext context,
            IErrorLogService logService
        )
        {
            _context = context;
            this.errorLogService = logService;
        }

        public async Task<ICollection<BetListingReport>> GetAll(bool? IsCorrect = null)
        {
            try
            {
                if (IsCorrect.HasValue) return await _context.BetListingReports.Include(i => i.ReportedBy).Include(i => i.ReportedListing).Where(a => a.IsCorrect == IsCorrect).ToListAsync().ConfigureAwait(false);
                else return await _context.BetListingReports.Include(i => i.ReportedBy).Include(i => i.ReportedListing).ToListAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<ICollection<BetListingReport>> GetAllByListingId(Guid Id, bool? IsCorrect = null)
        {
            try
            {
                if (IsCorrect.HasValue) return await _context.BetListingReports.Include(i => i.ReportedBy).Include(i => i.ReportedListing).Where(a => a.ReportedListingId == Id && a.IsCorrect == IsCorrect).ToListAsync().ConfigureAwait(false);
                else return await _context.BetListingReports.Include(i => i.ReportedBy).Include(i => i.ReportedListing).Where(a => a.ReportedListingId == Id).ToListAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<BetListingReport> GetById(Guid id)
        {
            try
            {
                return await _context.BetListingReports.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }


        public async Task<bool> Insert(BetListingReport item)
        {
            try
            {
                if (item != null)
                {
                    item.RowDate = DateTime.Now;
                    _context.BetListingReports.Add(item);
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


        public async Task<bool> Update(BetListingReport item)
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
                BetListingReport item = await _context.BetListingReports.Include(i => i.ReportedListing).Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
                if (item != null)
                {
                    BetListing listing = item.ReportedListing;
                    if (listing.IsReported)
                    {
                        if (!(await _context.BetListingReports.Where(b => b.ReportedListingId == listing.Id && b.IsCorrect && b.Id != item.Id).AnyAsync().ConfigureAwait(false)))
                        {
                            listing.IsReported = false;
                            _context.Entry(listing).State = EntityState.Modified;
                        }
                    }
                    var res = _context.BetListingReports.Remove(item);
                    
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
                BetListingReport item = await _context.BetListingReports.Include(i => i.ReportedListing).Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
                if (item != null)
                {
                    item.IsCorrect = newStatus;
                    BetListing listing = item.ReportedListing;
                    if (newStatus)
                    {
                        if (!listing.IsReported) {
                            listing.IsReported = true;
                            _context.Entry(listing).State = EntityState.Modified;
                        }
                        
                    }
                    else {
                        if (!(await _context.BetListingReports.Where(b => b.ReportedListingId == listing.Id && b.IsCorrect && b.Id != item.Id).AnyAsync().ConfigureAwait(false))) {
                            listing.IsReported = false;
                            _context.Entry(listing).State = EntityState.Modified;
                        }
                    }
                    _context.Entry(item).State = EntityState.Modified;
                }
                await _context.SaveChangesAsync().ConfigureAwait(false);
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