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
    public class UserRatingService : IUserRatingService
    {
        private DataContext _context;
        private readonly IErrorLogService errorLogService;

        public UserRatingService
        (
            DataContext context,
            IErrorLogService logService
        )
        {
            _context = context;
            this.errorLogService = logService;
        }

        public async Task<ICollection<UserRating>> GetAll()
        {
            try
            {
                return await _context.UserRatings.ToListAsync().ConfigureAwait(false);

            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }



        public async Task<ICollection<UserRating>> GetByFromId(Guid fromId)
        {
            try
            {
                return await _context.UserRatings.Where(c => c.FromId == fromId).ToListAsync().ConfigureAwait(false);

            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<ICollection<UserRating>> GetByToId(Guid toId)
        {
            try
            {
                return await _context.UserRatings.Where(c => c.ToId == toId).ToListAsync().ConfigureAwait(false);

            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<int> GetAverageRating(Guid toId)
        {
            try
            {
                var data = await _context.UserRatings.Where(c => c.ToId == toId).ToListAsync().ConfigureAwait(false);
                if (data.Any())
                {
                    return Convert.ToInt32(data.Average(i => i.RatingValue));
                }
                else return 0;


            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }


        public async Task<int> GetAverageRatingCorrect(Guid userId)
        {
            try
            {
                var correctPredictions = await _context.BetListings.Where(c => c.PostedbyId == userId && c.IsCorrect.HasValue && c.IsCorrect.Value).ToListAsync().ConfigureAwait(false);
                if (correctPredictions.Any())
                {
                    int totalCorrectPredictions = correctPredictions.Count;
                    if (totalCorrectPredictions <= 0)
                    {
                        return 0;
                    }
                    else if (totalCorrectPredictions >= 1 || totalCorrectPredictions <= 100)
                    {
                        return 1;
                    }
                    else if (totalCorrectPredictions >= 101 || totalCorrectPredictions <= 1000)
                    {
                        return 2;
                    }
                    else if (totalCorrectPredictions >= 1001 || totalCorrectPredictions <= 2000)
                    {
                        return 3;
                    }
                    else if (totalCorrectPredictions >= 2001 || totalCorrectPredictions <= 5000)
                    {
                        return 4;
                    }
                    else
                    {
                        return 5;
                    }
                }
                else return 0;


            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<int> GetTotalRatingPoints(Guid userId)
        {
            try
            {
                var correctPredictions = await _context.BetListings.Where(c => c.PostedbyId == userId && c.IsCorrect.HasValue && c.IsCorrect.Value).ToListAsync().ConfigureAwait(false);
                if (correctPredictions.Any())
                {
                    int totalCorrectPredictions = correctPredictions.Count;
                    return totalCorrectPredictions;

                }
                else return 0;


            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }
        public async Task<UserRating> GetByFromTo(Guid fromId, Guid toId)
        {
            try
            {
                return await _context.UserRatings.Where(c => c.FromId == fromId && c.ToId == toId).SingleOrDefaultAsync().ConfigureAwait(false);

            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<UserRating> GetById(int id)
        {
            try
            {
                return await _context.UserRatings.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }


        public async Task<bool> Insert(UserRating item)
        {
            try
            {
                if (item != null)
                {
                    item.RowDate = DateTime.UtcNow;
                    _context.UserRatings.Add(item);
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


        public async Task<bool> Update(UserRating item)
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
                UserRating item = await _context.UserRatings.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
                if (item != null)
                {
                    var res = _context.UserRatings.Remove(item);
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