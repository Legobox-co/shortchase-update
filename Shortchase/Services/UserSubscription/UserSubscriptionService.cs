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
    public class UserSubscriptionService : IUserSubscriptionService
    {
        private DataContext _context;
        private readonly IErrorLogService errorLogService;

        public UserSubscriptionService
        (
            DataContext context,
            IErrorLogService logService
        )
        {
            _context = context;
            this.errorLogService = logService;
        }

        public async Task<ICollection<UserSubscription>> GetAll(bool? IsActive = null)
        {
            try
            {
                if (IsActive.HasValue) return await _context.UserSubscriptions.Where(a => a.SubscriptionEnd <= DateTime.UtcNow).ToListAsync().ConfigureAwait(false);
                else return await _context.UserSubscriptions.ToListAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }


        public async Task<ICollection<UserSubscription>> GetAllFromType(string Type, bool? IsActive = null)
        {
            try
            {
                if (IsActive.HasValue) return await _context.UserSubscriptions.Where(a => a.Type == Type && a.SubscriptionEnd <= DateTime.UtcNow).ToListAsync().ConfigureAwait(false);
                else return await _context.UserSubscriptions.Where(a => a.Type == Type).ToListAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<ICollection<UserSubscription>> GetAllFromUser(Guid id, bool? IsActive = null)
        {
            try
            {

                if (IsActive.HasValue) return await _context.UserSubscriptions.Include(i => i.User).Include(i => i.Subscription).Where(c => !c.Deleted && c.UserId == id && c.SubscriptionEnd <= DateTime.UtcNow).ToListAsync().ConfigureAwait(false);
                else return await _context.UserSubscriptions.Include(i => i.User).Include(i => i.Subscription).Where(c => !c.Deleted && c.UserId == id).ToListAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<UserSubscription> GetById(Guid id)
        {
            try
            {
                return await _context.UserSubscriptions.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }



        public async Task<bool> Insert(UserSubscription item)
        {
            try
            {
                if (item != null)
                {
                    item.RowDate = DateTime.UtcNow;
                    item.Deleted = false;
                    item.HasBeenAutoRenewed = false;
                    item.AutoRenew = true;
                    _context.UserSubscriptions.Add(item);
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


        public async Task<bool> Update(UserSubscription item)
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
                UserSubscription item = await _context.UserSubscriptions.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
                if (item != null)
                {
                    item.Deleted = true;
                    item.SubscriptionDeleteDate = DateTime.UtcNow;

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


        public async Task<bool> Cancel(UserSubscription item)
        {
            try
            {
                if (item != null)
                {
                    item.PaymentStatus = UserSubscriptionPaymentStatus.Cancelled;
                    item.SubscriptionCancelDate = DateTime.UtcNow;

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

        public async Task<bool> UserHasActiveSubscriptionPlan(UserSubscription item)
        {
            try
            {
                if (item != null)
                {
                    var result = await _context.UserSubscriptions
                        .Where
                        (
                            u => item.UserId == u.UserId 
                            && item.Type == u.Type 
                            && !u.Deleted 
                            && (u.SubscriptionStart.Date <= item.SubscriptionEnd.Date && item.SubscriptionStart.Date <= u.SubscriptionEnd.Date) 
                            && u.PaymentStatus != UserSubscriptionPaymentStatus.Cancelled 
                            && u.PaymentStatus != UserSubscriptionPaymentStatus.Rejected
                        ).AnyAsync().ConfigureAwait(false);
                    return result;
                }
                else throw new Exception("No subscription given");
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return false;
            }
        }


        public async Task<UserSubscription> GetActiveSubscriptionPlan(Guid UserId, string Type)
        {
            try
            {
                var result = await _context.UserSubscriptions.Include(u => u.GiftBy).Include(u => u.User).Include(u => u.Subscription).Where(u => UserId == u.UserId && Type == u.Type && !u.Deleted && u.SubscriptionEnd.Date >= DateTime.UtcNow.Date && u.PaymentStatus != UserSubscriptionPaymentStatus.Cancelled && u.PaymentStatus != UserSubscriptionPaymentStatus.Rejected).SingleOrDefaultAsync().ConfigureAwait(false);
                return result;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return null;
            }
        }




        public async Task<ICollection<UserSubscription>> GetSubscriptionsToRenew()
        {
            try
            {
                var result = await _context.UserSubscriptions
                    .Include(u => u.User).Include(u => u.Subscription)
                    .Where(
                        u => 
                            !u.Deleted 
                            && u.SubscriptionEnd.Date <= DateTime.UtcNow.AddDays(1).Date 
                            && u.SubscriptionEnd.Date >= DateTime.UtcNow.AddDays(-1).Date 
                            && u.PaymentStatus != UserSubscriptionPaymentStatus.Cancelled 
                            && u.PaymentStatus != UserSubscriptionPaymentStatus.Rejected
                            && u.AutoRenew
                            && !u.HasBeenAutoRenewed
                            && !string.IsNullOrWhiteSpace(u.PaypalSubscriptionId)
                    )
                    .ToListAsync().ConfigureAwait(false);
                return result;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return null;
            }
        }

    }
}