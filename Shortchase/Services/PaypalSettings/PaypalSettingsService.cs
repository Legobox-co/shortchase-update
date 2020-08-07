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
    public class PaypalSettingsService : IPaypalSettingsService
    {
        private DataContext _context;
        private readonly IErrorLogService errorLogService;

        public PaypalSettingsService
        (
            DataContext context,
            IErrorLogService logService
        )
        {
            _context = context;
            this.errorLogService = logService;
        }

        public async Task<ICollection<PaypalSettings>> GetAll()
        {
            try
            {
                return await _context.PaypalSettings.OrderByDescending(o => o.RowDate).ToListAsync().ConfigureAwait(false);
                
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }



        public async Task<PaypalSettings> GetDefault()
        {
            try
            {
                return await _context.PaypalSettings.Where(c => c.IsDefault).OrderByDescending(o => o.RowDate).FirstOrDefaultAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<PaypalSettings> GetById(int id)
        {
            try
            {
                return await _context.PaypalSettings.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }


        public async Task<bool> Insert(PaypalSettings item)
        {
            try
            {
                if (item != null)
                {
                    var checkIfThereIsDefault = await _context.PaypalSettings.Where(i => i.IsDefault).SingleOrDefaultAsync().ConfigureAwait(false);
                    if (checkIfThereIsDefault != null && item.IsDefault) {
                        checkIfThereIsDefault.IsDefault = false;
                        _context.Entry(checkIfThereIsDefault).State = EntityState.Modified;
                    }
                    item.RowDate = DateTime.UtcNow;
                    _context.PaypalSettings.Add(item);
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



        public async Task<bool> Update(PaypalSettings item)
        {
            try
            {
                if (item != null)
                {
                    var checkIfThereIsDefault = await _context.PaypalSettings.Where(i => i.IsDefault).SingleOrDefaultAsync().ConfigureAwait(false);
                    if (checkIfThereIsDefault != null && item.IsDefault)
                    {
                        checkIfThereIsDefault.IsDefault = false;
                        _context.Entry(checkIfThereIsDefault).State = EntityState.Modified;
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

        public async Task<bool> Delete(int id)
        {
            try
            {
                PaypalSettings item = await _context.PaypalSettings.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
                if (item != null)
                {
                    var res = _context.PaypalSettings.Remove(item);
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
                PaypalSettings item = await _context.PaypalSettings.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
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