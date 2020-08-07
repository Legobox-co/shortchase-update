using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shortchase.Dtos;
using Shortchase.Entities;

namespace Shortchase.Services
{
    public interface IMediaFolderService
    {
        
        Task<ICollection<MediaFolder>> GetAll(bool? isEnabled = null);
        Task<ICollection<MediaFolder>> GetAllByParentId(Guid? Id);
        Task<MediaFolder> GetById(Guid id);

        Task<bool> Insert(MediaFolder MediaFolder);
        Task<bool> Update(MediaFolder MediaFolder);
        Task<bool> Delete(Guid id);

        Task<bool> SwitchStatus(Guid id, bool newStatus);

    }
}