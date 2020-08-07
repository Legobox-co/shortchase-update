using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shortchase.Entities;

namespace Shortchase.Services
{
    public partial interface IErrorLogService
    {
        Task<bool> InsertException(string error, string Method, string Path, Guid? User = null);

        Task<bool> InsertException(Exception e, Guid? User = null, [System.Runtime.CompilerServices.CallerMemberName] string Method = "", [System.Runtime.CompilerServices.CallerFilePath] string Path = "");
        Task<string> InsertExceptionTest(Exception e, Guid? User = null, [System.Runtime.CompilerServices.CallerMemberName] string Method = "", [System.Runtime.CompilerServices.CallerFilePath] string Path = "");

        Task<ICollection<ErrorLog>> GetAll();

        Task<IQueryable<ErrorLog>> GetAllQ();
    }
}