using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Shortchase.Entities;

namespace Shortchase.Services
{
    public partial interface ISMSSenderService
    {

        Task<bool> SendSMS(string Recipient, string Message);

    }
}