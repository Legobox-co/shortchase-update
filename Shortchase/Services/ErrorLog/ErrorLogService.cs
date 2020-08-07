using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shortchase.Entities;
using Shortchase.Helpers;

namespace Shortchase.Services
{
    public partial class ErrorLogService : IErrorLogService
    {
        private readonly DataContext db;

        public ErrorLogService(DataContext context)
        {
            this.db = context;
        }

        public async Task<bool> InsertException(string error, string Method, string Path, Guid? User = null)
        {
            try
            {
                ErrorLog exception = new ErrorLog
                {
                    RowDate = DateTime.Now,
                    Log = error,
                    Method = Method,
                    Path = Path,
                    User = User
                };
                db.ErrorLogs.Add(exception);
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> InsertException(Exception e, Guid? User = null, [System.Runtime.CompilerServices.CallerMemberName] string Method = "", [System.Runtime.CompilerServices.CallerFilePath] string Path = "")
        {
            try
            {
                string log = e.Message;

                Exception ex = e.InnerException;
                while (ex != null && !string.IsNullOrWhiteSpace(ex.Message))
                {
                    log = log + " -> " + ex.Message;
                    ex = ex.InnerException;
                }
                await InsertException(log, Method, Path, User);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        public async Task<string> InsertExceptionTest(Exception e, Guid? User = null, [System.Runtime.CompilerServices.CallerMemberName] string Method = "", [System.Runtime.CompilerServices.CallerFilePath] string Path = "")
        {
            try
            {
                string log = e.Message;

                Exception ex = e.InnerException;
                while (ex != null && !string.IsNullOrWhiteSpace(ex.Message))
                {
                    log = log + " -> " + ex.Message;
                    ex = ex.InnerException;
                }
                await InsertException(log, Method, Path, User);
                return "deu bao";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<ICollection<ErrorLog>> GetAll()
        {
            try
            {
                return await db.ErrorLogs.ToListAsync();
            }
            catch (Exception e)
            {
                if (!await (InsertException(e))) throw;
                else return null;
            }
        }

        public async Task<IQueryable<ErrorLog>> GetAllQ()
        {
            try
            {
                return db.ErrorLogs;
            }
            catch (Exception e)
            {
                if (!await (InsertException(e))) throw;
                else return null;
            }
        }
    }
}