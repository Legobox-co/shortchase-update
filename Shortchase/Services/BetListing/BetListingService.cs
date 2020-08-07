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
    public class BetListingService : IBetListingService
    {
        private DataContext _context;
        private readonly IErrorLogService errorLogService;

        public BetListingService
        (
            DataContext context,
            IErrorLogService logService
        )
        {
            _context = context;
            this.errorLogService = logService;
        }

        public async Task<ICollection<BetListing>> GetAll(bool? isDeleted = null)
        {
            try
            {
                if (isDeleted.HasValue) return await _context.BetListings.Include(i => i.Category).Include(i => i.SubCategory).Include(i => i.Postedby).Include(i => i.Reports).Include(i => i.Tip).Include(i => i.Market).Include(i => i.Pick).Include(i => i.Bookmaker).Where(a => a.Deleted == isDeleted).ToListAsync().ConfigureAwait(false);
                else return await _context.BetListings.Include(i => i.Category).Include(i => i.SubCategory).Include(i => i.Postedby).Include(i => i.Reports).ToListAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<BetListing> GetById(Guid id)
        {
            try
            {
                return await _context.BetListings.Include(i => i.SubCategory).Include(i => i.Category).Include(i => i.Market).Include(i => i.Pick).Include(i => i.Tip).Include(i => i.Bookmaker).Include(i => i.Postedby).Include(i => i.Postedby.ToRatings)
                    .Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }


        public async Task<bool> IsListingUnderLimits(BetListing listing)
        {
            try
            {
                int listingsCount = await _context.BetListings.CountAsync(i => i.PostedbyId == listing.PostedbyId && i.PickType == listing.PickType && i.RowDate >= DateTime.UtcNow.AddHours(-24) && !i.Deleted).ConfigureAwait(false);
                if (listingsCount <= 0) return true;
                else
                {
                    if (listing.PickType == BetListingType.Free)
                    {
                        if (listingsCount < BetListingLimits.Free) return true;
                        else return false;
                    }
                    else if (listing.PickType == BetListingType.Premium)
                    {
                        if (listingsCount < BetListingLimits.Premium) return true;
                        else return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }


        public async Task<bool> IsBoisterouListing(BetListing listing)
        {
            try
            {
                int categoryTotal = await _context.BetListings.Include(i => i.Category).Include(i => i.SubCategory).Include(i => i.Bookmaker).Include(i => i.Market).Include(i => i.Tip).Include(i => i.Pick)
                    .Where
                    (
                        i =>
                            i.PostedbyId == listing.PostedbyId
                            && i.CategoryId == listing.CategoryId
                            && !i.Deleted
                    ).CountAsync().ConfigureAwait(false);
                int categoryTotalCorrect = await _context.BetListings
                    .Where
                    (
                        i =>
                            i.PostedbyId == listing.PostedbyId
                            && i.CategoryId == listing.CategoryId
                            && !i.Deleted
                            && i.IsCorrect.HasValue && i.IsCorrect.Value
                    ).CountAsync().ConfigureAwait(false);


                int subcategoryTotal = 1;
                int subcategoryTotalCorrect = 1;
                if (listing.SubCategoryId.HasValue)
                {
                    subcategoryTotal = await _context.BetListings
                        .Where
                        (
                            i =>
                                i.PostedbyId == listing.PostedbyId
                                && i.SubCategoryId.Value == listing.SubCategoryId.Value
                                && !i.Deleted
                        ).CountAsync().ConfigureAwait(false);
                    subcategoryTotalCorrect = await _context.BetListings
                    .Where
                    (
                        i =>
                            i.PostedbyId == listing.PostedbyId
                            && i.SubCategoryId.Value == listing.SubCategoryId.Value
                            && !i.Deleted
                            && i.IsCorrect.HasValue && i.IsCorrect.Value
                    ).CountAsync().ConfigureAwait(false);
                }

                Tip listingTip = await _context.Tips.Where(i => i.Id == listing.TipId).SingleOrDefaultAsync();
                if (listingTip == null) throw new Exception("No valid Tip was provided");

                int predictionTotal = await _context.BetListings.Include(i => i.Tip)
                    .Where
                    (
                        i =>
                            i.PostedbyId == listing.PostedbyId
                            && i.Tip.Name == listingTip.Name
                            && !i.Deleted
                    ).CountAsync().ConfigureAwait(false);
                int predictionTotalCorrect = await _context.BetListings.Include(i => i.Tip)
                    .Where
                    (
                        i =>
                            i.PostedbyId == listing.PostedbyId
                            && i.Tip.Name == listingTip.Name
                            && !i.Deleted
                            && i.IsCorrect.HasValue && i.IsCorrect.Value
                    ).CountAsync().ConfigureAwait(false);


                int oddsTotal = await _context.BetListings
                    .Where
                    (
                        i =>
                            i.PostedbyId == listing.PostedbyId
                            && Convert.ToDecimal(i.Odds) >= (Convert.ToDecimal(listing.Odds) - 0.1m) && Convert.ToDecimal(i.Odds) <= (Convert.ToDecimal(listing.Odds) + 0.1m)
                            && !i.Deleted
                    ).CountAsync().ConfigureAwait(false);
                int oddsTotalCorrect = await _context.BetListings
                    .Where
                    (
                        i =>
                            i.PostedbyId == listing.PostedbyId
                            && Convert.ToDecimal(i.Odds) >= (Convert.ToDecimal(listing.Odds) - 0.1m) && Convert.ToDecimal(i.Odds) <= (Convert.ToDecimal(listing.Odds) + 0.1m)
                            && !i.Deleted
                            && i.IsCorrect.HasValue && i.IsCorrect.Value
                    ).CountAsync().ConfigureAwait(false);


                Pick listingPick = await _context.Picks.Where(i => i.Id == listing.PickId).SingleOrDefaultAsync();
                if (listingPick == null) throw new Exception("No valid Pick was provided");
                int team1Total = await _context.BetListings.Include(i => i.Pick)
                    .Where
                    (
                        i =>
                            i.PostedbyId == listing.PostedbyId
                            && i.Pick.Team1 == listingPick.Team1
                            && !i.Deleted
                    ).CountAsync().ConfigureAwait(false);
                int team1TotalCorrect = await _context.BetListings
                    .Where
                    (
                        i =>
                            i.PostedbyId == listing.PostedbyId
                            && i.Pick.Team1 == listingPick.Team1
                            && !i.Deleted
                            && i.IsCorrect.HasValue && i.IsCorrect.Value
                    ).CountAsync().ConfigureAwait(false);


                int team2Total = await _context.BetListings.Include(i => i.Pick)
                    .Where
                    (
                        i =>
                            i.PostedbyId == listing.PostedbyId
                            && i.Pick.Team2 == listingPick.Team2
                            && !i.Deleted
                    ).CountAsync().ConfigureAwait(false);
                int team2TotalCorrect = await _context.BetListings
                    .Where
                    (
                        i =>
                            i.PostedbyId == listing.PostedbyId
                            && i.Pick.Team2 == listingPick.Team2
                            && !i.Deleted
                            && i.IsCorrect.HasValue && i.IsCorrect.Value
                    ).CountAsync().ConfigureAwait(false);

                decimal categoryPercentage = 0.00m;
                decimal subcategoryPercentage = 0.00m;
                decimal predictionPercentage = 0.00m;
                decimal oddsPercentage = 0.00m;
                decimal team1Percentage = 0.00m;
                decimal team2Percentage = 0.00m;

                if (categoryTotal != 0)
                {
                    categoryPercentage = Convert.ToDecimal(categoryTotalCorrect) / Convert.ToDecimal(categoryTotal);
                }

                if (subcategoryTotal != 0)
                {
                    subcategoryPercentage = Convert.ToDecimal(subcategoryTotalCorrect) / Convert.ToDecimal(subcategoryTotal);
                }

                if (predictionTotal != 0)
                {
                    predictionPercentage = Convert.ToDecimal(predictionTotalCorrect) / Convert.ToDecimal(predictionTotal);
                }

                if (oddsTotal != 0)
                {
                    oddsPercentage = Convert.ToDecimal(oddsTotalCorrect) / Convert.ToDecimal(oddsTotal);
                }

                if (team1Total != 0)
                {
                    team1Percentage = Convert.ToDecimal(team1TotalCorrect) / Convert.ToDecimal(team1Total);
                }
                if (team2Total != 0)
                {
                    team2Percentage = Convert.ToDecimal(team2TotalCorrect) / Convert.ToDecimal(team2Total);
                }

                if
                (
                    categoryPercentage >= 0.95m
                    && subcategoryPercentage >= 0.95m
                    && predictionPercentage >= 0.95m
                    && oddsPercentage >= 0.95m
                    && team1Percentage >= 0.95m
                    && team2Percentage >= 0.95m
                )
                {
                    return true;
                }
                else return false;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }


        public async Task<bool> Insert(BetListing item)
        {
            try
            {
                if (item != null)
                {
                    item.RowDate = DateTime.UtcNow;
                    _context.BetListings.Add(item);
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


        public async Task<bool> Update(BetListing item)
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
                BetListing item = await _context.BetListings.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
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

        
        public async Task<bool> DeleteBatch(Guid[] Ids)
        {
            try
            {
                ICollection<BetListing> items = await _context.BetListings.Where(c => Ids.Contains(c.Id)).ToListAsync().ConfigureAwait(false);
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



        public async Task<bool> IncreaseViews(Guid id)
        {
            try
            {
                BetListing item = await _context.BetListings.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
                if (item != null)
                {
                    item.Views = item.Views + 1;
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

        public async Task<bool> SwitchStatus(Guid id, bool newStatus)
        {
            try
            {
                BetListing item = await _context.BetListings.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
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


        public async Task<ICollection<BetListing>> GetAllListingsByUser(Guid id)
        {
            try
            {
                return await _context.BetListings.Include(i => i.Category).Include(i => i.SubCategory).Include(i => i.Market).Include(i => i.Pick).Include(i => i.Tip).Include(i => i.Bookmaker).Include(i => i.Postedby).Where(c => c.PostedbyId == id && !c.Deleted).OrderByDescending(o => o.Pick.FinishTime).ThenBy(o => o.Pick.StartTime).ToListAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }



        public async Task<ICollection<BetListing>> GetAllAvailableForMarketplace(SearchFilters filters)
        {
            try
            {

                IOrderedQueryable<BetListing> dataSort = null;
                var data = _context.BetListings.Include(i => i.SubCategory).Include(i => i.Category).Include(i => i.Market).Include(i => i.Pick).Include(i => i.Tip).Include(i => i.Bookmaker).Include(i => i.Postedby).Include(i => i.Postedby.BetListings).Where(i => !i.Deleted && !i.IsReported && i.Pick.FinishTime > DateTime.UtcNow);
                if (filters != null)
                {
                    //Filtering Start
                    if (filters.Category.HasValue)
                    {
                        data = data.Where(i => i.CategoryId == filters.Category.Value);
                    }
                    if (filters.SubCategory.HasValue)
                    {
                        data = data.Where(i => i.SubCategoryId.HasValue && i.SubCategoryId.Value == filters.SubCategory.Value);
                    }
                    if (filters.PriceMin.HasValue)
                    {
                        data = data.Where(i => i.Price >= filters.PriceMin.Value);
                    }
                    if (filters.PriceMax.HasValue)
                    {
                        data = data.Where(i => i.Price <= filters.PriceMax.Value);
                    }
                    if (filters.Odds.HasValue)
                    {
                        data = data.Where(i => Convert.ToDecimal(i.Odds) >= (filters.Odds.Value - 0.1m) && Convert.ToDecimal(i.Odds) <= (filters.Odds.Value + 0.1m));
                    }
                    if (!string.IsNullOrWhiteSpace(filters.Keyword))
                    {
                        string Keyword = filters.Keyword.ToUpper();
                        data = data.Where(i => (i.PickType + i.Title + i.Description + i.Pick.Team1 + i.Pick.Team2 + i.Bookmaker.Description + i.Analysis + i.Tip.Name + i.Category.Name + i.Postedby.UserName + i.Postedby.FirstName + i.Postedby.LastName + (i.Postedby.FirstName + " " + i.Postedby.LastName)).ToUpper().Contains(Keyword) || (i.SubCategoryId.HasValue ? i.SubCategory.Name.ToUpper() : "").ToUpper().Contains(Keyword));
                    }

                    if (!string.IsNullOrWhiteSpace(filters.SortByPickType))
                    {
                        if (filters.SortByPickType == BetListingType.Live)
                        {
                            data = data.Where(o => o.PickType == BetListingType.Live);
                        }
                        else if (filters.SortByPickType == BetListingType.Premium)
                        {
                            data = data.Where(o => o.PickType == BetListingType.Premium);
                        }
                        else
                        {
                            data = data.Where(o => o.PickType == BetListingType.Free);
                        }
                    }
                    //----Filtering End


                    //Sorting Start
                    if (!string.IsNullOrWhiteSpace(filters.SortBy))
                    {
                        if (filters.SortBy == SortListingTypeType.Boisterous)
                        {
                            data = data.Where(o => o.IsBoisterousListing == true);
                        }
                    }

                    dataSort = data.OrderBy(o => 0);

                    if (!string.IsNullOrWhiteSpace(filters.SortBy))
                    {
                        if (!(filters.SortBy == SortListingTypeType.Boisterous))
                        {
                            dataSort = dataSort.ThenByDescending(o => o.RowDate);
                        }
                    }


                    if (!string.IsNullOrWhiteSpace(filters.SortByPrice))
                    {
                        if (filters.SortByPrice == SortOrderType.Ascending)
                        {
                            dataSort = dataSort.ThenBy(o => o.Price);
                        }
                        else
                        {
                            dataSort = dataSort.ThenByDescending(o => o.Price);
                        }
                    }



                    if (!string.IsNullOrWhiteSpace(filters.SortByOdds))
                    {
                        if (filters.SortByOdds == SortOrderType.Ascending)
                        {
                            dataSort = dataSort.ThenBy(o => Convert.ToDecimal(o.Odds));
                        }
                        else
                        {
                            dataSort = dataSort.ThenByDescending(o => Convert.ToDecimal(o.Odds));
                        }
                    }
                    //----Sorting End
                }

                dataSort = dataSort.ThenBy(o => o.IsProCapperListing);
                return await dataSort.ToListAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }


        public async Task<ICollection<BetListing>> GetAllRelatedAvailableForMarketplace(Guid PostedBy, Guid ExcludeListingId)
        {
            try
            {
                var data = _context.BetListings.Include(i => i.SubCategory).Include(i => i.Category).Include(i => i.Market).Include(i => i.Pick).Include(i => i.Tip).Include(i => i.Bookmaker).Include(i => i.Postedby).Include(i => i.Postedby.ToRatings)
                    .Where(i => !i.Deleted && !i.IsReported && i.Pick.FinishTime > DateTime.UtcNow && i.PostedbyId == PostedBy && i.Id != ExcludeListingId).OrderBy(o => o.IsProCapperListing).ThenByDescending(o => o.RowDate).Take(12);

                return await data.ToListAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }




        public async Task<ICollection<BetListing>> GetAllAvailableForDropdownSearch(string Keyword)
        {
            try
            {

                IOrderedQueryable<BetListing> dataSort = null;
                var data = _context.BetListings.Include(i => i.SubCategory).Include(i => i.Category).Include(i => i.Market).Include(i => i.Pick).Include(i => i.Tip).Include(i => i.Bookmaker).Include(i => i.Postedby).Include(i => i.Postedby.ToRatings).Where(i => !i.Deleted && !i.IsReported && i.Pick.FinishTime > DateTime.UtcNow);

                if (!string.IsNullOrWhiteSpace(Keyword))
                {
                    string KeywordUpper = Keyword.ToUpper();
                    data = data.Where(i => (i.PickType + i.Title + i.Description + i.Pick.Team1 + i.Pick.Team2 + i.Bookmaker.Description + i.Analysis + i.Tip.Name + i.Category.Name + i.Postedby.UserName + i.Postedby.FirstName + i.Postedby.LastName+ (i.Postedby.FirstName +" "+ i.Postedby.LastName)).ToUpper().Contains(KeywordUpper) || (i.SubCategoryId.HasValue ? i.SubCategory.Name.ToUpper() : "").ToUpper().Contains(KeywordUpper));
                }



                return await data.ToListAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }



        public async Task<ICollection<BetListing>> GetAllWaitingForValidation()
        {
            try
            {
                return await _context.BetListings.Include(i => i.Category).Include(i => i.Market).Include(i => i.Tip).Include(i => i.Pick).Where(i => !i.Deleted && !i.DateVerifiedByApi.HasValue && !i.IsCorrect.HasValue && i.Pick.FinishTime < DateTime.UtcNow && !string.IsNullOrWhiteSpace(i.Category.APIURL)).ToListAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }
    }
}