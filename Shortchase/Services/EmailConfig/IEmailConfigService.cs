using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shortchase.Entities;

namespace Shortchase.Services
{
    public partial interface IEmailConfigService
    {
        Task<ICollection<EmailConfig>> GetAll();

        Task<IQueryable<EmailConfig>> GetAllQ();

        Task<EmailConfig> GetById(int id);

        Task Insert(EmailConfig emailConfig);

        Task Update(EmailConfig emailConfig);

        Task Delete(EmailConfig emailConfig);

        Task<EmailConfig> ConfigurationSystemEmail();

        Task<EmailAccount> GetDefault();

        Task<EmailAccount> GetByName(string name);
    }
}