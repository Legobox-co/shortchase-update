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
    public class AddressService : IAddressService
    {
        private DataContext _context;
        private readonly IErrorLogService errorLogService;

        public AddressService
        (
            DataContext context,
            IErrorLogService logService
        )
        {
            _context = context;
            this.errorLogService = logService;
        }

        public async Task<ICollection<Address>> GetAll(bool? isEnabled = null, bool? NeedsDependantData = null)
        {
            try
            {
                if (isEnabled.HasValue)
                {
                    if (NeedsDependantData.HasValue && NeedsDependantData.Value) return await _context.Addresses.Where(a => a.IsEnabled == isEnabled).Include(a => a.Country).ToListAsync().ConfigureAwait(false);
                    else return await _context.Addresses.Where(a => a.IsEnabled == isEnabled).ToListAsync().ConfigureAwait(false);
                }
                else
                {
                    if (NeedsDependantData.HasValue && NeedsDependantData.Value) return await _context.Addresses.Include(a => a.Country).ToListAsync().ConfigureAwait(false);
                    else return await _context.Addresses.ToListAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<Address> GetById(int id)
        {
            try
            {
                return await _context.Addresses.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }


        public async Task<Address> GetPrimaryAddress()
        {
            try
            {
                return await _context.Addresses.Include(i => i.Country).Where(c => c.IsPrimary).SingleOrDefaultAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }


        public async Task<bool> Insert(Address address)
        {
            try
            {
                if (address != null)
                {
                    address.RowDate = DateTime.Now;
                    _context.Addresses.Add(address);
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


        public async Task<bool> Update(Address address)
        {
            try
            {
                if (address != null)
                {
                    _context.Entry(address).State = EntityState.Modified;
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
                Address item = await _context.Addresses.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
                if (item != null)
                {
                    var res = _context.Addresses.Remove(item);
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
                Address item = await _context.Addresses.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
                if (item != null)
                {
                    item.IsEnabled = newStatus;
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


        public async Task<bool> SwitchPrimaryAddress(int id)
        {
            try
            {
                Address item = await _context.Addresses.Where(c => c.IsPrimary).SingleOrDefaultAsync().ConfigureAwait(false);
                Address newPrimaryAddress = await _context.Addresses.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
                if (item != null)
                {
                    item.IsPrimary = false;
                    _context.Entry(item).State = EntityState.Modified;
                    if (newPrimaryAddress != null)
                    {
                        newPrimaryAddress.IsPrimary = true;
                        _context.Entry(newPrimaryAddress).State = EntityState.Modified;
                    }
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                }
                else {
                    if (newPrimaryAddress != null)
                    {
                        newPrimaryAddress.IsPrimary = true;
                        _context.Entry(newPrimaryAddress).State = EntityState.Modified;
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