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
    public class POTDListingPredictionService : IPOTDListingPredictionService
    {
        private DataContext _context;
        private readonly IErrorLogService errorLogService;

        public POTDListingPredictionService
        (
            DataContext context,
            IErrorLogService logService
        )
        {
            _context = context;
            this.errorLogService = logService;
        }

        public async Task<ICollection<POTDListingPrediction>> GetAll(bool? isDeleted = null)
        {
            try
            {
                if (isDeleted.HasValue) return await _context.POTDListingPredictions.Include(i => i.POTD).Include(i => i.PredictedBy).Where(a => a.Deleted == isDeleted).ToListAsync().ConfigureAwait(false);
                else return await _context.POTDListingPredictions.Include(i => i.POTD).Include(i => i.PredictedBy).ToListAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }
        public async Task<ICollection<PredictionComment>> GetAllComment()
        {
            try
            {
                return await _context.PredictionComments.ToListAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }
        public async Task<POTDListingPrediction> GetById(Guid id)
        {
            try
            {
                return await _context.POTDListingPredictions.Include(i => i.POTD).Include(i => i.PredictedBy).Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }


        public async Task<ICollection<POTDListingPrediction>> GetByUserId(Guid id)
        {
            try
            {
                return await _context.POTDListingPredictions.Include(i => i.POTD).Include(i => i.PredictedBy).Where(c => c.PredictedById == id).ToListAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }



        public async Task<ICollection<POTDListingPrediction>> GetByPOTDId(Guid id)
        {
            try
            {
                return await _context.POTDListingPredictions.Include(i => i.POTD).Include(i => i.PredictedBy).Include(i => i.Market).Include(i => i.Tip).Where(c => c.POTDId == id).ToListAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<POTDListingPrediction> GetUserPredictionForPOTD(Guid id, Guid potdId)
        {
            try
            {
                return await _context.POTDListingPredictions.Include(i => i.POTD).Include(i => i.PredictedBy).Where(c => c.PredictedById == id && c.POTDId == potdId).SingleOrDefaultAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }


        public async Task<bool> Insert(POTDListingPrediction item)
        {
            try
            {
                bool result = false;
                if (item != null)
                {
                    var UserHasOtherRecordsForThisPOTD = await _context.POTDListingPredictions.AnyAsync(p => p.POTDId == item.POTDId && p.PredictedById == item.PredictedById).ConfigureAwait(false);
                    if (UserHasOtherRecordsForThisPOTD) throw new Exception("Already predicted this POTD!");
                    else
                    {
                        item.RowDate = DateTime.UtcNow;
                        _context.POTDListingPredictions.Add(item);
                        await _context.SaveChangesAsync().ConfigureAwait(false);
                        result = true;
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }
        public async Task<bool> InsertComment(PredictionComment item)
        {
            try
            {
                bool result = false;
                if (item != null)
                {
                    
                        //item.RowDate = DateTime.UtcNow;
                        _context.PredictionComments.Add(item);
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

        public async Task<bool> Update(POTDListingPrediction item)
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
                POTDListingPrediction item = await _context.POTDListingPredictions.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
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



        public async Task<bool> Validate(Guid id, bool Value)
        {
            try
            {
                POTDListingPrediction item = await _context.POTDListingPredictions.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
                if (item != null)
                {

                    item.VerifiedAsCorrect = Value;
                    item.DateVerified = DateTime.UtcNow;
                    if (Value)
                    {
                        User user = await _context.Users.Where(i => i.Id == item.PredictedById).SingleOrDefaultAsync().ConfigureAwait(false);
                        user.TotalPointsAvailable += POTDPoints.CorrectPrediction;
                        _context.Entry(user).State = EntityState.Modified;
                    }
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


        public async Task<bool> Validate(ICollection<POTDListingPrediction> predictions)
        {
            try
            {
                Guid firstPredictionPOTDId = (predictions.FirstOrDefault()).POTDId;
                POTDListing potd = await _context.POTDListings.Where(i => i.Id == firstPredictionPOTDId).SingleOrDefaultAsync().ConfigureAwait(false);
                foreach (var prediction in predictions)
                {
                    if (prediction.MarketId == potd.MarketId && prediction.TipId == potd.TipId)
                    {
                        prediction.VerifiedAsCorrect = true;
                        prediction.DateVerified = DateTime.UtcNow;
                        User user = await _context.Users.Where(i => i.Id == prediction.PredictedById).SingleOrDefaultAsync().ConfigureAwait(false);
                        user.TotalPointsAvailable += POTDPoints.CorrectPrediction;
                        _context.Entry(user).State = EntityState.Modified;

                    }
                    else {
                        prediction.VerifiedAsCorrect = false;
                        prediction.DateVerified = DateTime.UtcNow;
                    }
                    _context.Entry(prediction).State = EntityState.Modified;
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




        public async Task<ICollection<POTDListingPrediction>> GetAllWaitingValidation()
        {
            try
            {
                return await _context.POTDListingPredictions.Include(i => i.POTD).ThenInclude(x => x.Pick).Include(i => i.PredictedBy).Include(i => i.Market).Include(i => i.Tip).Where(c => !c.Deleted && !c.DateVerified.HasValue && !c.VerifiedAsCorrect.HasValue && DateTime.UtcNow > c.POTD.Pick.StartTime && DateTime.UtcNow > c.POTD.Pick.FinishTime).ToListAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

    }
}