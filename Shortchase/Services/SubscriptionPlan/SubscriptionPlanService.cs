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
    public class SubscriptionPlanService : ISubscriptionPlanService
    {
        private DataContext _context;
        private readonly IErrorLogService errorLogService;

        public SubscriptionPlanService
        (
            DataContext context,
            IErrorLogService logService
        )
        {
            _context = context;
            this.errorLogService = logService;
        }

        public async Task<ICollection<SubscriptionPlan>> GetAll(bool? IsActive = null)
        {
            try
            {
                if (IsActive.HasValue) return await _context.SubscriptionPlans.Where(a => a.IsActive == IsActive.Value).ToListAsync().ConfigureAwait(false);
                else return await _context.SubscriptionPlans.ToListAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }


        public async Task<ICollection<SubscriptionPlan>> GetAllFromType(string Type, bool? IsActive = null)
        {
            try
            {
                if (IsActive.HasValue) return await _context.SubscriptionPlans.Where(a => a.Type == Type && a.IsActive == IsActive.Value).ToListAsync().ConfigureAwait(false);
                else return await _context.SubscriptionPlans.Where(a => a.Type == Type).ToListAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<SubscriptionPlan> GetById(int id)
        {
            try
            {
                return await _context.SubscriptionPlans.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }



        public async Task<bool> Insert(SubscriptionPlan item)
        {
            try
            {
                if (item != null)
                {
                    item.RowDate = DateTime.Now;
                    _context.SubscriptionPlans.Add(item);
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


        public async Task<bool> Update(SubscriptionPlan item)
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


        public async Task<bool> UpdatePaypalProductCreated(SubscriptionPlan item)
        {
            try
            {
                if (item != null)
                {
                    _context.Entry(item).State = EntityState.Modified;

                    var otherItemsToUpdate = await _context.SubscriptionPlans.Where(i => i.Type == item.Type).ToListAsync().ConfigureAwait(true);

                    if (otherItemsToUpdate.Count > 0) {
                        foreach (var otherItem in otherItemsToUpdate) {
                            otherItem.PaypalProductId = item.PaypalProductId;
                            otherItem.PaypalProductName = item.PaypalProductName;
                            _context.Entry(otherItem).State = EntityState.Modified;
                        }
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



        public async Task<bool> Delete(int id)
        {
            try
            {
                SubscriptionPlan item = await _context.SubscriptionPlans.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
                if (item != null)
                {
                    var res = _context.SubscriptionPlans.Remove(item);
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
                SubscriptionPlan item = await _context.SubscriptionPlans.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
                if (item != null)
                {
                    item.IsActive = newStatus;
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