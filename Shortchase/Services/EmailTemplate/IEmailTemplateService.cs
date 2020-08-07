using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shortchase.Entities;

namespace Shortchase.Services
{
    public partial interface IEmailTemplateService
    {
        Task<ICollection<EmailTemplate>> GetAll();

        Task<IQueryable<EmailTemplate>> GetAllQ();

        Task<EmailTemplate> GetById(int id);

        Task Insert(EmailTemplate emailTemplate);

        Task Update(EmailTemplate emailTemplate);

        Task Delete(EmailTemplate emailTemplate);

        Task<ICollection<EmailTemplate>> SearchByName(string name);

        Task<EmailTemplate> GetByName(string name);
    }
}