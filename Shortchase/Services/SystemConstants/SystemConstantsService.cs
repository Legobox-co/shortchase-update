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
    public class SystemConstantsservice : ISystemConstantsService
    {
        private DataContext _context;
        private readonly IErrorLogService errorLogService;

        public SystemConstantsservice
        (
            DataContext context,
            IErrorLogService logService
        )
        {
            _context = context;
            this.errorLogService = logService;
        }

        public async Task<SystemConstants> GetById(int id)
        {
            try
            {
                return await _context.SystemConstants.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }
        
        public async Task<SystemConstants> GetByName(string name)
        {
            try
            {
                return await _context.SystemConstants.Where(c => c.Name == name).SingleOrDefaultAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }



        public async Task<bool> Update(SystemConstants item)
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
        
        public async Task<bool> Insert(SystemConstants item)
        {
            try
            {
                if (item != null)
                {
                    _context.SystemConstants.Add(item);
                    _context.Entry(item).State = EntityState.Added;
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
        
        public async Task<bool> UpdateFeeAndTaxes(decimal fees, decimal taxes, decimal boisterousFees)
        {
            try
            {
                SystemConstants ServiceFee = await GetByName(SystemConstantName.RegularFees).ConfigureAwait(false);
                SystemConstants BoisterousServiceFee = await GetByName(SystemConstantName.BoisterousFees).ConfigureAwait(false);
                SystemConstants Taxes = await GetByName(SystemConstantName.Taxes).ConfigureAwait(false);

                ServiceFee.Value = fees.ToString();
                Taxes.Value = taxes.ToString();
                BoisterousServiceFee.Value = boisterousFees.ToString();

                _context.Entry(ServiceFee).State = EntityState.Modified;
                _context.Entry(Taxes).State = EntityState.Modified;
                _context.Entry(BoisterousServiceFee).State = EntityState.Modified;
                await _context.SaveChangesAsync().ConfigureAwait(false);

                
                return true;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return false;
            }
        }


        public async Task<bool> UpdateSessionTimeout(int value)
        {
            try
            {
                SystemConstants SessionTimeout = await GetByName(SystemConstantName.SessionTimeout).ConfigureAwait(false);

                SessionTimeout.Value = value.ToString();

                _context.Entry(SessionTimeout).State = EntityState.Modified;
                await _context.SaveChangesAsync().ConfigureAwait(false);


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