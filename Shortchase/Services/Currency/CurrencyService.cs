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
    public class CurrencyService : ICurrencyService
    {
        private DataContext _context;
        private readonly IErrorLogService errorLogService;

        public CurrencyService
        (
            DataContext context,
            IErrorLogService logService
        )
        {
            _context = context;
            this.errorLogService = logService;
        }

        public async Task<ICollection<Currency>> GetAll(bool? isDeleted = null)
        {
            try
            {
                if (isDeleted.HasValue) return await _context.Currencies.Where(a => a.Deleted == isDeleted).ToListAsync().ConfigureAwait(false);
                else return await _context.Currencies.ToListAsync().ConfigureAwait(false);
                
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<Currency> GetById(Guid id)
        {
            try
            {
                return await _context.Currencies.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }



        public async Task<bool> Insert(Currency currency)
        {
            try
            {
                if (currency != null)
                {
                    currency.RowDate = DateTime.Now;
                    _context.Currencies.Add(currency);
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


        public async Task<bool> Update(Currency currency)
        {
            try
            {
                if (currency != null)
                {
                    _context.Entry(currency).State = EntityState.Modified;
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
                Currency item = await _context.Currencies.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
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
        public async Task<bool> Activate(Guid id)
        {
            try
            {
                Currency item = await _context.Currencies.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
                if (item != null)
                {
                    item.Deleted = false;
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
                var items = await _context.Currencies.Where(c => Ids.Contains(c.Id)).ToListAsync().ConfigureAwait(false);
                if (items != null && items.Count > 0)
                {
                    foreach (var item in items)
                    {
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



    }
}