using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shortchase.Entities;
using Shortchase.Helpers;

namespace Shortchase.Services
{
    public partial class EmailTemplateService : IEmailTemplateService
    {
        private readonly DataContext db;
        private readonly IErrorLogService errorLogService;

        public EmailTemplateService(DataContext context, IErrorLogService errorLogService)
        {
            this.db = context;
            this.errorLogService = errorLogService;
        }

        public async Task<ICollection<EmailTemplate>> GetAll()
        {
            try
            {
                return await db.EmailTemplates.OrderByDescending(q => q.RowDate).ToListAsync();
            }
            catch (Exception e)
            {
                if (!await errorLogService.InsertException(e)) throw;
                return null;
            }
        }

        public async Task<IQueryable<EmailTemplate>> GetAllQ()
        {
            try
            {
                return db.EmailTemplates.OrderByDescending(q => q.RowDate);
            }
            catch (Exception e)
            {
                if (!await errorLogService.InsertException(e).ConfigureAwait(false)) throw;
                return null;
            }
        }

        public async Task<ICollection<EmailTemplate>> SearchByName(string name)
        {
            try
            {
                return await db.EmailTemplates.Where(e => e.Name.Contains(name, StringComparison.InvariantCulture)).OrderBy(q => q.Name).ToListAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                if (!await errorLogService.InsertException(e).ConfigureAwait(false)) throw;
                else return null;
            }
        }

        public async Task<EmailTemplate> GetByName(string name)
        {
            try
            {
                return await db.EmailTemplates.Where(e => e.Name.ToUpper() == name.ToUpper()).FirstOrDefaultAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                if (!await errorLogService.InsertException(e).ConfigureAwait(false)) throw;
                else return null;
            }
        }

        public async Task<EmailTemplate> GetById(int id)
        {
            try
            {
                return await db.EmailTemplates.Where(m => m.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                if (!await errorLogService.InsertException(e).ConfigureAwait(false)) throw;
                return null;
            }
        }

        public async Task Insert(EmailTemplate emailTemplate)
        {
            try
            {
                if (emailTemplate != null)
                {
                    emailTemplate.RowDate = DateTime.Now;
                    db.EmailTemplates.Add(emailTemplate);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e);
                throw;
            }
        }

        public async Task Update(EmailTemplate emailTemplate)
        {
            try
            {
                var record = db.EmailTemplates.Where(q => q.Id == emailTemplate.Id).FirstOrDefault();
                record.Body = emailTemplate.Body;
                record.Subject = emailTemplate.Subject;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e);
                throw;
            }
        }

        public async Task Delete(EmailTemplate emailTemplate)
        {
            try
            {
                var record = db.EmailTemplates.Where(q => q.Id == emailTemplate.Id).FirstOrDefault();
                db.EmailTemplates.Remove(record);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e);
                throw;
            }
        }
    }
}