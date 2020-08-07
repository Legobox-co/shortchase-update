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
    public class POTDListingLiveReportingInteractionService : IPOTDListingLiveReportingInteractionService
    {
        private DataContext _context;
        private readonly IErrorLogService errorLogService;

        public POTDListingLiveReportingInteractionService
        (
            DataContext context,
            IErrorLogService logService
        )
        {
            _context = context;
            this.errorLogService = logService;
        }


        public async Task<POTDListingLiveReportingInteraction> GetById(Guid id)
        {
            try
            {
                return await _context.POTDListingLiveReportingInteractions.Include(i => i.POTDLiveReport).Include(i => i.InteractedBy).Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<ICollection<POTDListingLiveReportingInteraction>> GetByPOTDLiveReportingAndType( Guid POTDLiveReportingId, string Type)
        {
            try
            {
                return await _context.POTDListingLiveReportingInteractions.Include(i => i.POTDLiveReport).Include(i => i.InteractedBy).Where(c => c.POTDLiveReportId == POTDLiveReportingId && c.InteractionType == Type).ToListAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<ICollection<POTDListingLiveReportingInteraction>> GetByUserAndPOTDLiveReportingId(Guid UserId, Guid POTDLiveReportingId)
        {
            try
            {
                return await _context.POTDListingLiveReportingInteractions.Include(i => i.POTDLiveReport).Include(i => i.InteractedBy).Where(c => c.InteractedById == UserId && c.POTDLiveReportId == POTDLiveReportingId).ToListAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }




        public async Task<bool> Insert(POTDListingLiveReportingInteraction item)
        {
            try
            {
                bool result = false;
                string exception = "";
                if (item != null)
                {
                    var userInteractions = await _context.POTDListingLiveReportingInteractions.Where(i => i.InteractedById == item.InteractedById && i.POTDLiveReportId == item.POTDLiveReportId).ToListAsync().ConfigureAwait(false);
                    bool canInsert = false;
                    if (userInteractions.Count <= 0)
                    {
                        canInsert = true;
                    }
                    else {
                        if (userInteractions.Count > 1)
                        {
                            canInsert = false;
                            exception = "More than 1 interactions found for this user";
                        }
                        else {
                            var interaction = userInteractions.FirstOrDefault();
                            if (interaction == null)
                            {
                                canInsert = false;
                                exception = "There should be 1 interaction found for this user, but none where retrieve on first or default.";
                            }
                            else {
                                if (interaction.InteractionType == item.InteractionType)
                                {
                                    _context.POTDListingLiveReportingInteractions.Remove(interaction);
                                    await _context.SaveChangesAsync().ConfigureAwait(false);
                                    return true;
                                }
                                else {
                                    interaction.RowDate = DateTime.UtcNow;
                                    interaction.InteractionType = item.InteractionType;
                                    _context.Entry(interaction).State = EntityState.Modified;
                                    await _context.SaveChangesAsync().ConfigureAwait(false);
                                    return true;
                                }
                            }
                        }
                    }

                    if (canInsert)
                    {
                        item.RowDate = DateTime.UtcNow;
                        _context.POTDListingLiveReportingInteractions.Add(item);
                        await _context.SaveChangesAsync().ConfigureAwait(false);
                        result = true;
                    }
                    else throw new Exception(exception);
                }
                return result;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }


        public async Task<bool> Update(POTDListingLiveReportingInteraction item)
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
                POTDListingLiveReportingInteraction item = await _context.POTDListingLiveReportingInteractions.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
                if (item != null)
                {
                    _context.POTDListingLiveReportingInteractions.Remove(item);
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