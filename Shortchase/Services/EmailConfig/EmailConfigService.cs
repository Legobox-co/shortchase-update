using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shortchase.Entities;
using Shortchase.Helpers;

namespace Shortchase.Services
{
    public partial class EmailConfigService : IEmailConfigService
    {
        private readonly DataContext db;
        private readonly IErrorLogService errorLogService;

        public EmailConfigService(DataContext context, IErrorLogService errorLogService)
        {
            this.db = context;
            this.errorLogService = errorLogService;
        }

        public async Task<ICollection<EmailConfig>> GetAll()
        {
            try
            {
                return await db.EmailConfigs.ToListAsync();
            }
            catch (Exception e)
            {
                if (!await errorLogService.InsertException(e)) throw;
                return null;
            }
        }

        public async Task<IQueryable<EmailConfig>> GetAllQ()
        {
            try
            {
                return db.EmailConfigs;
            }
            catch (Exception e)
            {
                if (!await errorLogService.InsertException(e)) throw;
                return null;
            }
        }

        public async Task<EmailConfig> GetById(int id)
        {
            try
            {
                return await db.EmailConfigs.Where(m => m.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                if (!await errorLogService.InsertException(e)) throw;
                return null;
            }
        }

        public async Task Insert(EmailConfig emailConfig)
        {
            try
            {
                if (emailConfig != null)
                {
                    emailConfig.RowDate = DateTime.Now;
                    db.EmailConfigs.Add(emailConfig);
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e);
                throw;
            }
        }

        public async Task Update(EmailConfig emailConfig)
        {
            try
            {
                var record = await db.EmailConfigs.Where(q => q.Id == emailConfig.Id).FirstOrDefaultAsync();
                record.Display_name = emailConfig.Display_name;
                record.Email = emailConfig.Email;
                record.Password = emailConfig.Password;
                record.Host = emailConfig.Host;
                record.Port = emailConfig.Port;
                record.User_name = emailConfig.User_name;
                record.Enable_ssl = emailConfig.Enable_ssl;
                record.Is_default_email_account = emailConfig.Is_default_email_account;
                record.Active = emailConfig.Active;
                await db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e);
                throw;
            }
        }

        public async Task Delete(EmailConfig emailConfig)
        {
            try
            {
                var record = await db.EmailConfigs.Where(q => q.Id == emailConfig.Id).FirstOrDefaultAsync();
                db.EmailConfigs.Remove(record);
                await db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e);
                throw;
            }
        }

        public async Task<EmailConfig> ConfigurationSystemEmail()
        {
            try
            {
                IQueryable<EmailConfig> query = from c in db.EmailConfigs where c.Is_default_email_account == true select c;
                var emailconfig = await query.FirstOrDefaultAsync();

                return emailconfig;
            }
            catch (Exception e)
            {
                if (!await errorLogService.InsertException(e)) throw;
                return null;
            }
        }

        public async Task<EmailAccount> GetDefault()
        {
            try
            {
                var emailconfig = await db.EmailConfigs.Where(i => i.Is_default_email_account == true).FirstOrDefaultAsync();
                var emailAccount = new EmailAccount();

                if (emailconfig != null)
                {
                    emailAccount.DisplayName = emailconfig.Display_name;
                    emailAccount.Email = emailconfig.Email;
                    emailAccount.Password = emailconfig.Password;
                    emailAccount.Host = emailconfig.Host;
                    emailAccount.Port = emailconfig.Port;
                    emailAccount.Username = emailconfig.User_name;
                    emailAccount.EnableSsl = emailconfig.Enable_ssl.Value;
                }

                return emailAccount;
            }
            catch (Exception e)
            {
                if (!await errorLogService.InsertException(e).ConfigureAwait(false)) throw;
                return null;
            }
        }

        public async Task<EmailAccount> GetByName(string name)
        {
            try
            {
                var emailconfig = await db.EmailConfigs.Where(i => i.Display_name.ToUpperInvariant() == name.ToUpperInvariant()).FirstOrDefaultAsync().ConfigureAwait(false);
                var emailAccount = new EmailAccount();

                return new EmailAccount
                {
                    DisplayName = emailconfig.Display_name,
                    Email = emailconfig.Email,
                    Password = emailconfig.Password,
                    Host = emailconfig.Host,
                    Port = emailconfig.Port,
                    Username = emailconfig.User_name,
                    EnableSsl = emailconfig.Enable_ssl.Value,
                    Id = emailconfig.Id
                };
            }
            catch (Exception e)
            {
                if (!await errorLogService.InsertException(e)) throw;
                return null;
            }
        }
    }
}