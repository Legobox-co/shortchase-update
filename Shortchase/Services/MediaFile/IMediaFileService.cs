using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shortchase.Dtos;
using Shortchase.Entities;

namespace Shortchase.Services
{
    public interface IMediaFileService
    {
        
        Task<ICollection<MediaFile>> GetAll(bool? isEnabled = null);
        Task<ICollection<MediaFile>> GetAllFolderId(Guid? Id);
        Task<MediaFile> GetById(Guid id);

        Task<bool> Insert(MediaFile MediaFile);
        Task<bool> Update(MediaFile MediaFile);
        Task<bool> Delete(Guid id);

        Task<bool> SwitchStatus(Guid id, bool newStatus);

    }
}