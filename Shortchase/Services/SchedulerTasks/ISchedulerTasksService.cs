using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shortchase.Dtos;
using Shortchase.Entities;

namespace Shortchase.Services
{
    public interface ISchedulerTasksService
    {
        Task ValidateByAPIJob();
        Task RenewSubscriptions();
    }
}