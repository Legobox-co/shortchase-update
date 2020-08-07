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
    public class MessageService : IMessageService
    {
        private DataContext _context;
        private readonly IErrorLogService errorLogService;

        public MessageService
        (
            DataContext context,
            IErrorLogService logService
        )
        {
            _context = context;
            this.errorLogService = logService;
        }

        public async Task<ICollection<Message>> GetAll(bool? IsRead = null)
        {
            try
            {
                if (IsRead.HasValue) return await _context.Messages.Include(i => i.From).Include(i => i.To).Where(a => IsRead.Value ? a.DateRead.HasValue : !a.DateRead.HasValue).ToListAsync().ConfigureAwait(false);
                else return await _context.Messages.Include(i => i.From).Include(i => i.To).ToListAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }
       
        public async Task<Message> GetById(int id)
        {
            try
            {
                return await _context.Messages.Include(i => i.From).Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }


        public async Task<bool> Insert(Message item)
        {
            try
            {
                if (item != null)
                {
                    item.RowDate = DateTime.UtcNow;
                    _context.Messages.Add(item);
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


        public async Task<bool> Update(Message item)
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

        public async Task<bool> Delete(int id)
        {
            try
            {
                Message item = await _context.Messages.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
                if (item != null)
                {
                    var res = _context.Messages.Remove(item);
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


        public async Task<ICollection<Message>> GetAllForUser(Guid UserId)
        {
            try
            {
                return await _context.Messages.Include(i => i.From).Include(i => i.To).Where(i => i.ToId == UserId).OrderBy(o => o.RowDate).ToListAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }


        public async Task<ICollection<MessagesListDto>> GetAllUsersThatHaveMessages()
        {
            try
            {
                var MessagesList2 = await _context.Users.Include(i => i.ToMessages).Where(i => i.ToMessages.Count > 0).Select(i => new MessagesList2Dto { UserId = i.Id, UserName = $"{i.FirstName} {i.LastName}", UnreadMessagesCount = i.ToMessages.Where(m => !m.DateRead.HasValue).Select(x => x.Id) }).ToListAsync().ConfigureAwait(false);
                return MessagesList2.Select(i => new MessagesListDto { UserId = i.UserId, UserName = i.UserName, UnreadMessagesCount = i.UnreadMessagesCount.Count() }).ToList();
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }


        public async Task<bool> UpdateBatch(ICollection<Message> items)
        {
            try
            {
                if (items != null  && items.Count > 0)
                {
                    foreach (var item in items) {
                        _context.Entry(item).State = EntityState.Modified;
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