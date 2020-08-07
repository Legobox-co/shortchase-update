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
    public class POTDListingLiveReportService : IPOTDListingLiveReportService
    {
        private DataContext _context;
        private readonly IErrorLogService errorLogService;

        public POTDListingLiveReportService
        (
            DataContext context,
            IErrorLogService logService
        )
        {
            _context = context;
            this.errorLogService = logService;
        }

        public async Task<ICollection<POTDListingLiveReport>> GetAll(bool? isDeleted = null)
        {
            try
            {
                if (isDeleted.HasValue) return await _context.POTDListingLiveReports.Include(i => i.POTD).Include(i => i.ReportedBy).Include(i => i.UserInteractions).Where(a => a.Deleted == isDeleted).ToListAsync().ConfigureAwait(false);
                else return await _context.POTDListingLiveReports.Include(i => i.POTD).Include(i => i.ReportedBy).Include(i => i.UserInteractions).ToListAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<POTDListingLiveReport> GetById(Guid id)
        {
            try
            {
                return await _context.POTDListingLiveReports.Include(i => i.POTD).Include(i => i.ReportedBy).Include(i => i.UserInteractions).Where(c => c.Id == id && !c.Deleted).SingleOrDefaultAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }


        public async Task<ICollection<POTDListingLiveReport>> GetByUserId(Guid id)
        {
            try
            {
                return await _context.POTDListingLiveReports.Include(i => i.POTD).Include(i => i.ReportedBy).Include(i => i.UserInteractions).Where(c => c.ReportedById == id && !c.Deleted).ToListAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }



        public async Task<ICollection<POTDListingLiveReport>> GetByPOTDId(Guid id)
        {
            try
            {
                return await _context.POTDListingLiveReports.Include(i => i.POTD).Include(i => i.ReportedBy).Include(i => i.UserInteractions).Where(c => c.POTDId == id && !c.Deleted).ToListAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }


        public async Task<bool> Insert(POTDListingLiveReport item)
        {
            try
            {
                bool result = false;
                if (item != null)
                {
                    item.RowDate = DateTime.UtcNow;
                    _context.POTDListingLiveReports.Add(item);
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                    result = true;
                }
                return result;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }


        public async Task<bool> Update(POTDListingLiveReport item)
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
                POTDListingLiveReport item = await _context.POTDListingLiveReports.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
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




    }
}